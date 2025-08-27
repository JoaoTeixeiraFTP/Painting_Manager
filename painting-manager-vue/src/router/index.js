import { createRouter, createWebHistory } from "vue-router";
import Login from "../views/Login.vue";
import Dashboard from "../views/Dashboard.vue";

const routes = [
  { path: "/login", component: Login },
  { path: "/dashboard", component: Dashboard }, // Dashboard é rota principal
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
