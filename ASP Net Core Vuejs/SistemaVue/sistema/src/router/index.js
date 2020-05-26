import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../components/Home.vue';
import Categoria from '../components/Categoria.vue';
import Articulo from '../components/Articulo.vue';
import Rol from '../components/Rol.vue';
import Usuario from '../components/Usuario.vue';
import Cliente from '../components/Cliente.vue';
import Proveedor from '../components/Proveedor.vue';
import Login from '../components/Login.vue';
import store from '../store/index';


Vue.use(VueRouter);

const routes = [{
        path: '/',
        name: 'home',
        component: Home,
        meta: {
            administrador: true,
            almacenero: true,
            vendedor: true
        }
    }, {
        path: '/Categorias',
        name: 'categorias',
        component: Categoria,
        meta: {
            administrador: true,
            almacenero: true
        }
    }, {
        path: '/Articulos',
        name: 'articulos',
        component: Articulo,
        meta: {
            administrador: true,
            almacenero: true
        }
    }, {
        path: '/Roles',
        name: 'roles',
        component: Rol,
        meta: {
            administrador: true
        }
    }, {
        path: '/Usuarios',
        name: 'usuarios',
        component: Usuario,
        meta: {
            administrador: true
        }
    }, {
        path: '/Clientes',
        name: 'clientes',
        component: Cliente,
        meta: {
            administrador: true,
            vendedor: true
        }
    }, {
        path: '/Proveedores',
        name: 'proveedores',
        component: Proveedor,
        meta: {
            administrador: true,
            almacenero: true
        }
    }, {
        path: '/Login',
        name: 'login',
        component: Login,
        meta: {
            libre: true
        }
    }

];

//const router = new VueRouter({
var router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
});

router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.libre)) {
        next();
    } else if (store.state.usuario && store.state.usuario.rol == 'Administrador') {
        if (to.matched.some(record => record.meta.administrador)) {
            next();
        }
    } else if (store.state.usuario && store.state.usuario.rol == 'Almacenero') {
        if (to.matched.some(record => record.meta.almacenero)) {
            next();
        }
    } else if (store.state.usuario && store.state.usuario.rol == 'Vendedor') {
        if (to.matched.some(record => record.meta.vendedor)) {
            next();
        }
    } else {
        next({
            name: 'login'
        });
    }
});

export default router;