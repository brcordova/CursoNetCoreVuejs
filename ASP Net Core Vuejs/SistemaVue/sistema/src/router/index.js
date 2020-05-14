import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../components/Home.vue';
import Categoria from '../components/Categoria.vue';


Vue.use(VueRouter);

const routes = [{
        path: '/',
        name: 'home',
        component: Home
    }, {
        path: '/Categoria',
        name: 'categoria',
        component: Categoria
    }

];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
});

export default router;