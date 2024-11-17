<template>
  <el-dialog
    v-model="dialogVisible"
    :title="isEdit ? '编辑厂商' : '新增厂商'"
    width="500px"
  >
    <el-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      label-width="100px"
    >
      <el-form-item label="厂商编码" prop="manufacturerCode">
        <el-input
          v-model="formData.manufacturerCode"
          placeholder="请输入厂商编码"
        />
      </el-form-item>
      <el-form-item label="厂商名称" prop="name">
        <el-input v-model="formData.name" placeholder="请输入厂商名称" />
      </el-form-item>
      <el-form-item label="地址" prop="address">
        <el-input v-model="formData.address" placeholder="请输入地址" />
      </el-form-item>
      <el-form-item label="联系人" prop="contactPerson">
        <el-input v-model="formData.contactPerson" placeholder="请输入联系人" />
      </el-form-item>
      <el-form-item label="联系电话" prop="contactPhone">
        <el-input
          v-model="formData.contactPhone"
          placeholder="请输入联系电话"
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
import { ManufacturerService } from "@/core/services/manufacturer";

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
  manufacturerCode: "",
  name: "",
  address: "",
  contactPerson: "",
  contactPhone: ""
});

const formRules = {
  manufacturerCode: [
    { required: true, message: "请输入厂商编码", trigger: "blur" }
  ],
  name: [{ required: true, message: "请输入厂商名称", trigger: "blur" }]
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
          ? ManufacturerService.update
          : ManufacturerService.create;
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
