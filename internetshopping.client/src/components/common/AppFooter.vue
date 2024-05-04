<template>
    <div class="footer">
        <div class="content">
            <p>&copy; 2024 Спортивный мир. Все права защищены.</p>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Footer"
    }
</script>

<style scoped>
    .footer {
        width: 100%;
        background-color: #304156;
        height: 40px;
        position: fixed;
        bottom: 0;
        left: 0;
        text-align: center;
        z-index: 999;
    }

    .content {
        margin: 0 auto;
        max-width: 960px; /* Ширина контейнера */
        height: 100%;
        line-height: 40px;
        color: white;
    }
</style>
