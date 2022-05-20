function checkLocalStorage(key) {
    return window.localStorage.getItem(key);
}

function setLocalStorage(key, value) {
    window.localStorage.setItem(key, value);
}