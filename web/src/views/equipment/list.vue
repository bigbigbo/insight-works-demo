<template>
  <div class="equipment-list">
    <div class="header-actions mb-2">
      <el-button type="primary" @click="handleAdd">新增设备</el-button>
    </div>

    <query-table
      ref="tableRef"
      :api="EquipmentService.getList"
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
      <el-table-column label="操作" width="180" fixed="right">
        <template #default="{ row }">
          <el-button type="primary" link @click="handleEdit(row)"
            >编辑</el-button
          >
          <el-button type="danger" link @click="handleDelete(row)"
            >删除</el-button
          >
          <el-button type="success" link @click="handleSync(row)"
            >同步数据</el-button
          >
        </template>
      </el-table-column>
    </query-table>

    <equipment-form
      v-model:visible="dialogVisible"
      :is-edit="isEdit"
      :data="currentRow"
      @success="handleSuccess"
    />

    <data-sync-modal
      v-model:visible="syncDialogVisible"
      :data="currentRow"
      @success="handleSyncSuccess"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import QueryTable from "@/components/query-table/index.vue";
import { EquipmentService } from "@/core/services/equipment";
import { ElMessage, ElMessageBox } from "element-plus";
import EquipmentForm from "./components/equipment-form.vue";
import DataSyncModal from "@/components/equipment-sync-modal/index.vue";

defineOptions({
  name: "EquipmentList"
});

const tableRef = ref();
const dialogVisible = ref(false);
const syncDialogVisible = ref(false);
const isEdit = ref(false);
const currentRow = ref({});

const columns = [
  {
    prop: "equipmentCode",
    label: "设备编号",
    width: 200
  },
  {
    prop: "manufacturer.name",
    label: "厂商名称",
    width: 180
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
  },
  {
    prop: "formattedUpdatedAt",
    label: "更新时间",
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
  ElMessageBox.confirm("确认删除该设备吗?", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      try {
        const res = await EquipmentService.delete({ id: row.id });
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

const handleSync = row => {
  currentRow.value = row;
  syncDialogVisible.value = true;
};

const handleSuccess = () => {
  tableRef.value?.refresh();
};

const handleSyncSuccess = () => {
  tableRef.value?.refresh();
};
</script>

<style scoped></style>
