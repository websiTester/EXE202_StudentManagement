// NỘI DUNG CUỐI CÙNG CHO sidebar.js
document.addEventListener('DOMContentLoaded', function () {
    const menuToggle = document.getElementById('menuToggle');
    const sidebarOverlay = document.getElementById('sidebar-overlay');
    const body = document.body;

    if (!menuToggle || !sidebarOverlay || !body) {
        console.error("Missing required elements for sidebar functionality.");
        return;
    }

    // Sự kiện khi nhấn nút menu
    menuToggle.addEventListener('click', function () {
        body.classList.toggle('sidebar-expanded');
    });

    // Sự kiện khi nhấn vào vùng mờ (overlay) để đóng
    sidebarOverlay.addEventListener('click', function () {
        body.classList.remove('sidebar-expanded');
    });

    // Tùy chọn: Đóng sidebar khi nhấn phím Escape
    document.addEventListener('keydown', function (event) {
        if (event.key === 'Escape' && body.classList.contains('sidebar-expanded')) {
            body.classList.remove('sidebar-expanded');
        }
    });
});