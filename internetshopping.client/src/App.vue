<template>
    <div class="app">
        <AppHeader />
        <router-view />
        <AppFooter />
    </div>
</template>

<script>
    import AppHeader from '../src/components/common/AppHeader.vue'
    import AppFooter from '../src/components/common/AppFooter.vue'

    export default {
        components: {
            AppHeader,
            AppFooter
        }
    }
</script>

<style scoped>
    .app {
        width: 100%;
        min-height: 100vh;
        display: flex;
        flex-direction: column;
        position: relative; /* ����� ���������� ����� ����������������� ������������ ����� ���������� */
    }

    /* ������������� AppHeader ������ */
    AppHeader {
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 999; /* ������������� z-index, ����� AppHeader ��� ������ ������ ��������� */
    }

    /* ������������� AppFooter ����� */
    AppFooter {
        position: fixed;
        bottom: 0;
        width: 100%;
        z-index: 999; /* ������������� z-index, ����� AppFooter ��� ������ ������ ��������� */
    }
</style>