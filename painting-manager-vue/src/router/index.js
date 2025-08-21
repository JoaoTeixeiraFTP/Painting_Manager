import { createRouter, createWebHistory } from "vue-router";
import Login from "../views/Login.vue";
import DashboardLayout from "../layouts/DashboardLayout.vue";

const routes = [
  { path: "/login", component: Login },
  {
    path: "/",
    component: DashboardLayout,
    children: [
      { path: "dashboard", component: () => import("../views/Dashboard.vue") },
      // { path: "users", component: () => import("@/views/Users.vue") },
      // { path: "roles", component: () => import("@/views/Roles.vue") },
      // { path: "groups", component: () => import("@/views/Groups.vue") },
      // { path: "colors", component: () => import("@/views/Colors.vue") },
      // { path: "components", component: () => import("@/views/Components.vue") },
      // { path: "formulas", component: () => import("@/views/Formulas.vue") },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
