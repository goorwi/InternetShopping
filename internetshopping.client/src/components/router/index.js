import { createRouter, createWebHistory } from 'vue-router'
import Home from '../pages/HomePage.vue'
import Catalog from '../pages/Catalog.vue'
import Cart from '../pages/Cart.vue'
import Register from '../pages/profile/Register.vue'
import Login from '../pages/profile/Login.vue'
import ProfileInfo from '../pages/profile/Customer-Info.vue'
import Orders from '../pages/profile/Customer-Orders.vue'
import Admin from '../pages/admin/Admin-Panel.vue'
import auth from '@/components/store/auth';



const routes = [
    { path: '/', component: Home },
    { path: '/Products', component: Catalog },
    { path: '/Cart', component: Cart },
    { path: '/Register', component: Register },
    { path: '/Login', component: Login },
    { path: '/info', component: ProfileInfo },
    { path: '/Orders', component: Orders },
    {
        path: '/admin', component: Admin,
        meta: { requiresAdmin: true }
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})
router.beforeEach((to, from, next) => {
    // Ïðîâåðÿåì ìåòà-ïîëå requiresAdmin è òåêóùåãî ïîëüçîâàòåëÿ ïðè êàæäîé íàâèãàöèè
    if (to.meta.requiresAdmin && (!auth.state.currentUser || !auth.state.currentUser.isAdmin)) {
        next({ path: '/login' }); // Åñëè òåêóùèé ïîëüçîâàòåëü íå ÿâëÿåòñÿ àäìèíèñòðàòîðîì, ïåðåíàïðàâëÿåì åãî íà ãëàâíóþ ñòðàíèöó
    } else {
        next(); // Ïðîäîëæàåì íàâèãàöèþ
    }
});

export default router