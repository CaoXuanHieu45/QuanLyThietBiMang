/** Tập trung các thao tác HTTP của màn hình thiết bị. */
export class DeviceApi {
    async request(url, options = {}) {
        const response = await fetch(url, {
            ...options,
            headers: { 'Content-Type': 'application/json', ...(options.headers || {}) }
        });

        if (!response.ok) {
            let detail = '';
            try { detail = (await response.json()).message || ''; } catch { /* Phản hồi không phải JSON. */ }
            throw new Error(detail || `Lỗi máy chủ (${response.status})`);
        }

        return response.status === 204 ? null : response.json();
    }

    getSites() { return this.request('/api/sites'); }
    getDevices() { return this.request('/api/devices'); }
    createDevice(device) {
        return this.request('/api/devices', { method: 'POST', body: JSON.stringify(device) });
    }
    updateDevice(id, device) {
        return this.request(`/api/devices/${id}`, { method: 'PUT', body: JSON.stringify(device) });
    }
    deleteDevice(id) { return this.request(`/api/devices/${id}`, { method: 'DELETE' }); }
}
