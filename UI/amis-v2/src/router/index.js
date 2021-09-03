import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

import DashBoard from "../components/content/DashBoard";
import Employee from "../components/content/Employee";



const routes = [
    { path: "/", component: DashBoard },
    { path: "/dashboard", component: DashBoard },
    { path: "/employee", component: Employee },
];

const router = new VueRouter({
    mode: 'history',
    routes,
});

export default router;