<template>
  <div class="product-model-list">
    <div class="header-actions mb-2">
      <el-button type="primary" @click="handleAdd">新增型号</el-button>
    </div>

    <query-table
      ref="tableRef"
      :api="ProductModelService.getList"
      :columns="columns"
      :show-index="true"
      :stripe="true"
    >
      <el-table-column
        v-for="col in columns"
        :key="col.prop"
        :prop="col.prop"
        :label="col.label"
        :width="col.width"
        show-overflow-tooltip
      />
      <el-table-column label="操作" width="114" fixed="right">
        <template #default="{ row }">
          <el-button type="primary" link @click="handleEdit(row)"
            >编辑</el-button
          >
          <el-button type="danger" link @click="handleDelete(row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </query-table>

    <product-model-form
      v-model:visible="dialogVisible"
      :is-edit="isEdit"
      :data="currentRow"
      @success="handleSuccess"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import QueryTable from "@/components/query-table/index.vue";
import { ProductModelService } from "@/core/services/product-model";
import { ElMessage, ElMessageBox } from "element-plus";
import ProductModelForm from "./components/product-model-form.vue";

defineOptions({
  name: "ProductModelList"
});

const tableRef = ref();
const dialogVisible = ref(false);
const isEdit = ref(false);
const currentRow = ref({});

const columns = [
  {
    prop: "modelCode",
    label: "型号编码",
    width: 200
  },
  {
    prop: "description",
    label: "描述"
  },
  {
    prop: "formattedCreatedAt",
    label: "创建时间",
    width: 180
  }
];

const handleAdd = () => {
  isEdit.value = false;
  currentRow.value = {};
  dialogVisible.value = true;
};

const handleEdit = row => {
  isEdit.value = true;
  currentRow.value = row;
  dialogVisible.value = true;
};

const handleDelete = row => {
  ElMessageBox.confirm("确认删除该型号吗?", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      try {
        const res = await ProductModelService.delete({ id: row.id });
        if (res.success) {
          ElMessage.success("删除成功");
          tableRef.value?.refresh();
        } else {
          ElMessage.error(res.message || "删除失败");
        }
      } catch (err) {
        ElMessage.error("删除失败");
      }
    })
    .catch(() => {
      ElMessage.info("已取消删除");
    });
};

const handleSuccess = () => {
  tableRef.value?.refresh();
};
</script>

<style scoped></style>
