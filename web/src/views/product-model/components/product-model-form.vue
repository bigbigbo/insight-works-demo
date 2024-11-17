<template>
  <el-dialog
    v-model="dialogVisible"
    :title="isEdit ? '编辑型号' : '新增型号'"
    width="500px"
  >
    <el-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      label-width="100px"
    >
      <el-form-item label="型号编码" prop="modelCode">
        <el-input v-model="formData.modelCode" placeholder="请输入型号编码" />
      </el-form-item>
      <el-form-item label="描述" prop="description">
        <el-input
          v-model="formData.description"
          type="textarea"
          placeholder="请输入描述"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="handleCancel">取消</el-button>
        <el-button type="primary" :loading="submitLoading" @click="handleSubmit"
          >确定</el-button
        >
      </span>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, watch } from "vue";
import { ElMessage } from "element-plus";
import { ProductModelService } from "@/core/services/product-model";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  isEdit: {
    type: Boolean,
    default: false
  },
  data: {
    type: Object,
    default: () => ({})
  }
});

const emit = defineEmits(["update:visible", "success"]);

const dialogVisible = ref(false);
const formRef = ref();
const submitLoading = ref(false);

const formData = ref({
  id: "",
  modelCode: "",
  description: ""
});

const formRules = {
  modelCode: [{ required: true, message: "请输入型号编码", trigger: "blur" }]
};

watch(
  () => props.visible,
  val => {
    dialogVisible.value = val;
  }
);

watch(
  () => dialogVisible.value,
  val => {
    emit("update:visible", val);
  }
);

watch(
  () => props.data,
  val => {
    // @ts-ignore
    formData.value = { ...val };
  },
  { deep: true }
);

const handleCancel = () => {
  dialogVisible.value = false;
  formRef.value?.resetFields();
};

const handleSubmit = async () => {
  if (!formRef.value) return;

  await formRef.value.validate(async (valid: boolean) => {
    if (valid) {
      try {
        submitLoading.value = true;
        const service = props.isEdit
          ? ProductModelService.update
          : ProductModelService.create;
        const res = await service(formData.value);
        if (res.success) {
          ElMessage.success(props.isEdit ? "更新成功" : "新增成功");
          dialogVisible.value = false;
          emit("success");
          formRef.value?.resetFields();
        } else {
          ElMessage.error(
            res.message || (props.isEdit ? "更新失败" : "新增失败")
          );
        }
      } catch (err) {
        ElMessage.error(props.isEdit ? "更新失败" : "新增失败");
      } finally {
        submitLoading.value = false;
      }
    }
  });
};
</script>

<style scoped></style>
