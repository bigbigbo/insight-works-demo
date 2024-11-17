export default {
  path: "/product-model",
  name: "ProductModel",
  meta: {
    icon: "ep:cpu",
    title: "产品管理",
    rank: 30
  },
  children: [
    {
      path: "/product-model/list",
      name: "ProductModelList",
      component: () => import("@/views/product-model/list.vue"),
      meta: {
        title: "型号列表",
        showParent: true
      }
    }
  ]
} satisfies RouteConfigsTable;
