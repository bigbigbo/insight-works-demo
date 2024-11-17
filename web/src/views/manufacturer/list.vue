<script setup lang="ts">
import { ref } from "vue";
import QueryTable from "@/components/query-table/index.vue";
import { ManufacturerService } from "@/core/services/manufacturer";
import { ElMessage, ElMessageBox } from "element-plus";
import ManufacturerForm from "./components/manufacturer-form.vue";

const tableRef = ref();
const dialogVisible = ref(false);
const isEdit = ref(false);
const currentRow = ref({});

const columns = [
  {
    prop: "manufacturerCode",
    label: "厂商编码",
    width: 120
  },
  {
    prop: "name",
    label: "厂商名称",
    width: 180
  },
  {
    prop: "address",
    label: "地址"
  },
  {
    prop: "contactPerson",
    label: "联系人",
    width: 120
  },
  {
    prop: "contactPhone",
    label: "联系电话",
    width: 120
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
  ElMessageBox.confirm("确认删除该厂商吗?", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      try {
        const res = await ManufacturerService.delete({ id: row.id });
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

<template>
  <div class="manufacturer-list">
    <div class="header-actions mb-2">
      <el-button type="primary" @click="handleAdd">新增厂商</el-button>
    </div>

    <query-table
      ref="tableRef"
      :api="ManufacturerService.getList"
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

    <manufacturer-form
      v-model:visible="dialogVisible"
      :is-edit="isEdit"
      :data="currentRow"
      @success="handleSuccess"
    />
  </div>
</template>

<style scoped></style>
