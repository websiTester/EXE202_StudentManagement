// wwwroot/js/sidebar.js
document.addEventListener("DOMContentLoaded", function () {
    const body = document.body;
    const sidebar = document.getElementById("sidebar");
    const btn = document.getElementById("menuToggle");
    const brand = document.getElementById("brandLink");

    let pinned = false;

    // Helper: set expanded based on pinned
    function setExpanded(expand) {
        if (expand) body.classList.add("sidebar-expanded");
        else body.classList.remove("sidebar-expanded");
    }

    // Toggle pinned (click menu)
    btn.addEventListener("click", (e) => {
        e.stopPropagation();
        pinned = !pinned;
        setExpanded(pinned);
    });

    // Hover behavior (temporary open when not pinned)
    sidebar.addEventListener("mouseenter", () => {
        if (!pinned) setExpanded(true);
    });
    sidebar.addEventListener("mouseleave", () => {
        if (!pinned) setExpanded(false);
    });

    // Click outside to close when not pinned
    document.addEventListener("click", (e) => {
        if (pinned) return; // pinned -> do nothing
        if (!sidebar.contains(e.target) && e.target !== btn) {
            setExpanded(false);
        }
    });

    // brand click => home
    if (brand) brand.addEventListener("click", (e) => {
        // allow normal navigation if it's an <a>
        // if you used a button, uncomment next line:
        // window.location.href = '/Home/StudentHome';
    });

    // Active item: match pathname or data-page
    const currentPath = window.location.pathname.toLowerCase();
    document.querySelectorAll("#sidebar .nav-link").forEach(link => {
        const href = (link.getAttribute("href") || "").toLowerCase();
        const dataPage = link.getAttribute("data-page");
        // if data-page provided, check that first
        if (dataPage && dataPage.toLowerCase() === (window.viewActivePage || "").toLowerCase()) {
            link.classList.add("active");
            return;
        }
        if (href === "/" && (currentPath === "/" || currentPath === "/home" || currentPath === "/home/index")) {
            link.classList.add("active");
            return;
        }
        if (href !== "/" && currentPath.startsWith(href)) {
            link.classList.add("active");
        }
    });

    // keyboard: Esc closes when not pinned
    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape" && !pinned) setExpanded(false);
    });
});
