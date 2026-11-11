import { DeviceApi } from './device-api.js';
import { DeviceForm } from './device-form.js';

/** Điều phối dữ liệu và hiển thị trang quản lý thiết bị. */
export class DevicePage {
    constructor() {
        this.api = new DeviceApi();
        this.devices = [];
        this.sites = [];
        this.form = new DeviceForm((id, device) => this.saveDevice(id, device));
        document.getElementById('addButton').onclick = () => this.addDevice();
        document.getElementById('refreshButton').onclick = () => this.loadData();
        document.getElementById('siteFilter').onchange = () => this.renderDevices();
        document.getElementById('searchInput').oninput = () => this.renderDevices();
    }

    notify(text, type = 'error') {
        const message = document.getElementById('message');
        message.textContent = text;
        message.className = type;
    }

    clearMessage() {
        const message = document.getElementById('message');
        message.textContent = '';
        message.className = '';
    }

    async loadData() {
        this.clearMessage();
        try {
            [this.sites, this.devices] = await Promise.all([this.api.getSites(), this.api.getDevices()]);
            this.fillSites();
            this.renderDevices();
        } catch (error) {
            this.notify(`Không tải được dữ liệu từ SQL Server. ${error.message} Kiểm tra SQL Server và quyền truy cập database QuanLyThietBiMang.`);
        }
    }

    fillSites() {
        const filter = document.getElementById('siteFilter');
        const form = document.getElementById('deviceSite');
        const selected = filter.value;
        filter.replaceChildren(new Option('Tất cả địa điểm', ''));
        form.replaceChildren();
        this.sites.forEach(site => {
            filter.add(new Option(site.name, site.id));
            form.add(new Option(site.name, site.id));
        });
        filter.value = selected;
    }

    renderDevices() {
        const body = document.getElementById('deviceRows');
        const query = document.getElementById('searchInput').value.trim().toLowerCase();
        const siteId = document.getElementById('siteFilter').value;
        const filtered = this.devices.filter(device =>
            (!siteId || String(device.siteId) === siteId) &&
            (!query || `${device.name} ${device.ipAddress}`.toLowerCase().includes(query))
        );
        body.replaceChildren();
        filtered.forEach((device, index) => this.renderRow(body, device, index));
        document.getElementById('emptyState').hidden = filtered.length > 0;
        document.getElementById('totalCount').textContent = this.devices.length;
        document.getElementById('onlineCount').textContent = this.devices.filter(d => d.status === 'Online').length;
        document.getElementById('offlineCount').textContent = this.devices.filter(d => d.status === 'Offline').length;
    }

    renderRow(body, device, index) {
        const row = body.insertRow();
        row.insertCell().textContent = index + 1;
        [device.name, device.ipAddress, device.siteName || ''].forEach(value => {
            row.insertCell().textContent = value;
        });
        const status = document.createElement('span');
        status.className = `pill ${device.status === 'Online' ? 'online' : 'offline'}`;
        status.textContent = device.status;
        row.insertCell().append(status);
        const actions = row.insertCell();
        const edit = document.createElement('button');
        edit.className = 'edit'; edit.textContent = 'Sửa'; edit.onclick = () => this.form.open(device);
        const remove = document.createElement('button');
        remove.className = 'danger'; remove.textContent = 'Xóa'; remove.onclick = () => this.deleteDevice(device);
        actions.append(edit, remove);
    }

    addDevice() {
        if (!this.sites.length) {
            this.notify('Chưa có địa điểm để gán thiết bị. Kiểm tra kết nối database.');
            return;
        }
        this.clearMessage();
        this.form.open();
    }

    async saveDevice(id, device) {
        try {
            if (id) await this.api.updateDevice(id, device);
            else await this.api.createDevice(device);
            this.form.close();
            this.notify(id ? 'Đã cập nhật thiết bị.' : 'Đã thêm thiết bị.', 'success');
            await this.loadData();
        } catch (error) {
            this.notify(`Không lưu được thiết bị: ${error.message}`);
        }
    }

    async deleteDevice(device) {
        if (!confirm(`Bạn có chắc muốn xóa thiết bị “${device.name}”?`)) return;
        try {
            await this.api.deleteDevice(device.id);
            this.notify('Đã xóa thiết bị.', 'success');
            await this.loadData();
        } catch (error) {
            this.notify(`Không xóa được thiết bị: ${error.message}`);
        }
    }
}
