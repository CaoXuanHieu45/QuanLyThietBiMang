/** Quản lý hộp thoại và dữ liệu nhập thiết bị. */
export class DeviceForm {
    constructor(onSubmit) {
        this.dialog = document.getElementById('deviceDialog');
        this.form = document.getElementById('deviceForm');
        this.onSubmit = onSubmit;
        this.form.addEventListener('submit', event => this.submit(event));
        document.getElementById('closeButton').onclick = () => this.close();
        document.getElementById('cancelButton').onclick = () => this.close();
    }

    open(device = null) {
        this.form.reset();
        document.getElementById('deviceId').value = device?.id || '';
        document.getElementById('formTitle').textContent = device ? 'Sửa thiết bị' : 'Thêm thiết bị';
        if (device) {
            document.getElementById('deviceName').value = device.name;
            document.getElementById('deviceIp').value = device.ipAddress;
            document.getElementById('deviceSite').value = device.siteId;
            document.getElementById('deviceStatus').value = device.status;
        }
        this.dialog.showModal();
    }

    close() { this.dialog.close(); }

    async submit(event) {
        event.preventDefault();
        const id = document.getElementById('deviceId').value;
        const device = {
            name: document.getElementById('deviceName').value.trim(),
            ipAddress: document.getElementById('deviceIp').value.trim(),
            siteId: Number(document.getElementById('deviceSite').value),
            status: document.getElementById('deviceStatus').value
        };
        await this.onSubmit(id, device);
    }
}
