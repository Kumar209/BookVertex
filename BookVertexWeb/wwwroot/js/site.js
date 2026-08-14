(function () {

    "use strict";

    const STORAGE_KEY = "bookvertex-theme";
    const DEFAULT_THEME = "bookvertex";

    function getSavedTheme() {
        return localStorage.getItem(STORAGE_KEY) || DEFAULT_THEME;
    }

    function applyTheme(theme) {

        if (!theme) {
            theme = DEFAULT_THEME;
        }

        document.documentElement.setAttribute(
            "data-theme",
            theme
        );

        localStorage.setItem(
            STORAGE_KEY,
            theme
        );

        updateActiveTheme(theme);
    }

    function updateActiveTheme(theme) {

        const themeButtons =
            document.querySelectorAll(".bv-theme-option");

        themeButtons.forEach(function (button) {

            const isActive =
                button.dataset.theme === theme;

            button.classList.toggle(
                "active",
                isActive
            );

            button.setAttribute(
                "aria-pressed",
                isActive.toString()
            );
        });
    }

    function closeThemeMenu(button) {

        const details =
            button.closest("details");

        if (details) {
            details.removeAttribute("open");
        }
    }

    document.addEventListener(
        "click",
        function (event) {

            const themeButton =
                event.target.closest(".bv-theme-option");

            if (themeButton) {

                const theme =
                    themeButton.dataset.theme;

                if (theme) {

                    applyTheme(theme);

                    closeThemeMenu(themeButton);
                }

                return;
            }

            document
                .querySelectorAll("details.dropdown[open]")
                .forEach(function (details) {

                    if (!details.contains(event.target)) {

                        details.removeAttribute("open");
                    }
                });
        }
    );

    function initializeTheme() {

        const savedTheme =
            getSavedTheme();

        applyTheme(savedTheme);
    }

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initializeTheme
        );

    } else {

        initializeTheme();
    }

})();