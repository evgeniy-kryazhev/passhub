function enableDarkMode(value) {
    if (value === true) {
        document.documentElement.classList.remove("theme-light");
        document.documentElement.classList.add("theme-dark");
    } else {
        document.documentElement.classList.remove("theme-dark");
        document.documentElement.classList.add("theme-light");
    }
}