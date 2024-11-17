export default {
  path: "/product-model",
  name: "ProductModel",
  meta: {
    icon: "ep:cpu",
    title: "产品型号",
    rank: 30
  },
  children: [
    {
      path: "list",
      name: "ProductModelList",
      component: () => import("@/views/product-model/index.vue"),
      meta: {
        title: "型号列表",
        showParent: true
      }
    }
  ]
} satisfies RouteConfigsTable;
