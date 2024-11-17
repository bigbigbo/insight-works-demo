<template>
  <div class="main">
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>产品型号管理</span>
        </div>
      </template>
      <div class="content">产品型号管理页面内容</div>
    </el-card>
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "ProductModelList"
});
</script>

<style scoped>
.main {
  margin: 0;
  padding: 0;
}

.box-card {
  width: 100%;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
