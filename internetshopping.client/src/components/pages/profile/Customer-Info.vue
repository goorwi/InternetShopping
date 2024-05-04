<template>
    <div>
        <Sidebar />
        <div class="main">
            <h2>Личная информация</h2>
            <form>
                <div class="mb-3">
                    <label for="firstname" class="form-label">Имя:</label>
                    <input type="text" id="firstname" v-model="currentUser.unique_name" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email:</label>
                    <input type="text" id="email" v-model="currentUser.email" class="form-control" disabled>
                </div>
                <div class="mb-3">
                    <label for="address" class="form-label">Address:</label>
                    <input type="text" id="address" v-model="currentUser.addressId" class="form-control">
                </div>
                <button @click.prevent="updateCustomer" class="btn btn-primary">Сохранить</button>
            </form>
        </div>
    </div>
</template>

<script>
    import Sidebar from '@/components/pages/profile/Profile-sidebar.vue';
    import axios from 'axios';
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    export default {
        components: {
            Sidebar
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };

        },
        methods: {
            async updateCustomer() {
                const patchOperations = [
                    { op: 'replace', path: '/Name', value: this.currentUser.unique_name },
                    { op: 'replace', path: '/Address', value: this.currentUser.address },
                ];
                try {
                    const response = await axios.patch(`customers/${this.currentUser.userId}/patch`, patchOperations);
                    const newToken = response.data.token;
                    localStorage.removeItem('token');
                    localStorage.setItem('token', newToken);
                } catch (error) {
                    console.error(error);
                }
            }
        },

    };
</script>

<style scoped>
    .main {
        margin-left: 200px;
        padding: 20px;
        background-color: #fff; /* Добавляем фон для основной области */
        border-radius: 8px; /* Округляем углы */
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Добавляем тень */
    }

    h2 {
        margin-bottom: 20px; /* Добавляем отступ снизу для заголовка */
    }

    .form-label {
        font-weight: bold; /* Делаем метки формы жирными */
    }

    .form-control {
        width: 100%; /* Расширяем поля ввода на всю доступную ширину */
        margin-bottom: 15px; /* Добавляем отступ снизу для элементов формы */
    }

    button.btn-primary {
        width: 100%; /* Расширяем кнопку на всю доступную ширину */
    }
</style>