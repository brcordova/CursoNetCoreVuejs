import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../components/Home.vue';
import Categoria from '../components/Categoria.vue';
import Articulo from '../components/Articulo.vue';


Vue.use(VueRouter);

const routes = [{
        path: '/',
        name: 'home',
        component: Home
    }, {
        path: '/Categoria',
        name: 'categoria',
        component: Categoria
    }, {
        path: '/Articulo',
        name: 'articulo',
        component: Articulo
    }

];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
});

export default router;