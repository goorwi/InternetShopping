<template>
    <main class="register">
        <section class="forms">
            <form class="register-form" @submit.prevent="register">
                <h2>Регистрация</h2>
                <div class="mb-3">
                    <label for="nickname" class="form-label">Имя</label>
                    <input type="text" id="nickname" class="form-control" v-model="nickname" />
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" id="email" class="form-control" v-model="email" />
                </div>
                <div class="mb-3">
                    <label for="address" class="form-label">Address</label>
                    <input type="text" id="address" class="form-control" v-model="address" />
                </div>
                <div class="mb-3">
                    <label for="phone" class="form-label">Phone</label>
                    <input type="tel" id="phone" class="form-control" v-model="phone" />
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Пароль</label>
                    <input type="password" id="passwordHash" class="form-control" v-model="passwordHash" />
                </div>
                <div class="mb-3">
                    <label for="confirmPassword" class="form-label">Подтвердите пароль</label>
                    <input type="password" id="confirmPassword" class="form-control" v-model="confirmPassword" />
                </div>
                <button type="submit" class="btn btn-primary">Регистрация</button>
            </form>
        </section>
    </main>
</template>

<script>
    export default {
        data() {
            return {
                email: '',
                passwordHash: '',
                nickname: '',
            };
        },
        methods: {
            register() {
                if (this.passwordHash !== this.confirmPassword) {
                    console.error('Passwords do not match');
                    return;
                }
                const user = {
                    Email: this.email,
                    Password: this.passwordHash,
                    IsAdmin: false,
                    Name: this.nickname,
                    Address: this.address,
                    Phone: this.phone
                };
                this.$store.dispatch('registerUser', user)
                    .then(() => {
                        this.$router.push('/Products');
                    })
                    .catch(error => {
                        console.error(error);
                        // Handle error
                    });
            },
        },
    };
</script>

<style scoped>
    .register {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }

    .forms {
        max-width: 400px;
    }

    .register-form {
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 5px;
    }

        .register-form h2 {
            margin-bottom: 20px;
        }
</style>