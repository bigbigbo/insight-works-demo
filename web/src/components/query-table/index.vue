<script setup lang="ts">
import { ref, onMounted, toRaw } from "vue";
import { ElTable, ElPagination } from "element-plus";

interface Props {
  // 请求接口方法
  api: (...args: any[]) => Promise<any>;
  // 请求参数
  params?: Record<string, any>;
  // 表格列配置
  columns: any[];
  // 是否自动加载数据
  immediate?: boolean;
  // 是否显示序号列
  showIndex?: boolean;
  // 是否显示斑马纹
  stripe?: boolean;
}

const props = withDefaults(defineProps<Props>(), {
  immediate: true,
  params: () => ({}),
  showIndex: false,
  stripe: false // 添加默认值
});

const emit = defineEmits(["update:loading"]);

// 表格数据
const tableData = ref<any[]>([]);
// 加载状态
const loading = ref(false);
// 分页信息
const pagination = ref({
  currentPage: 1,
  pageSize: 10,
  total: 0
});

// 获取表格数据
const getTableData = async () => {
  try {
    loading.value = true;
    emit("update:loading", true);

    const params = {
      pageIndex: pagination.value.currentPage,
      pageSize: pagination.value.pageSize,
      ...props.params
    };

    const res = await props.api(params);
    if (res?.success) {
      tableData.value = res.data?.items || [];
      pagination.value.total = res.data?.totalCount || 0;
    }
  } finally {
    loading.value = false;
    emit("update:loading", false);
  }
};

// 页码改变
const handleCurrentChange = (page: number) => {
  pagination.value.currentPage = page;
  getTableData();
};

// 每页条数改变
const handleSizeChange = (size: number) => {
  pagination.value.pageSize = size;
  pagination.value.currentPage = 1;
  getTableData();
};

// 刷新表格数据
const refresh = () => {
  pagination.value.currentPage = 1;
  getTableData();
};

// 暴露方法
defineExpose({
  refresh
});

onMounted(() => {
  if (props.immediate) {
    getTableData();
  }
});
</script>

<template>
  <div class="query-table">
    <el-table
      v-loading="loading"
      :data="tableData"
      :columns="columns"
      border
      style="width: 100%"
      :stripe="stripe"
    >
      <el-table-column
        v-if="showIndex"
        type="index"
        label="序号"
        width="60"
        align="center"
      />
      <slot />
    </el-table>

    <div class="pagination-container">
      <el-pagination
        v-model:current-page="pagination.currentPage"
        v-model:page-size="pagination.pageSize"
        :page-sizes="[10, 20, 50, 100]"
        :total="pagination.total"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div>
  </div>
</template>

<style scoped>
.query-table {
  width: 100%;
}

.pagination-container {
  margin-top: 15px;
  display: flex;
  justify-content: flex-end;
}
</style>
