class Helpers {
    static saveValue(key, data) {
        localStorage.setItem(key, data);
    }

    static getValue(key) {
        return localStorage.getItem(key);
    }
}