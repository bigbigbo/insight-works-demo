<template>
  <el-dialog v-model="dialogVisible" title="同步设备数据" width="500px">
    <el-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      label-width="100px"
    >
      <el-form-item label="数据类型" prop="syncType">
        <el-select v-model="formData.syncType" placeholder="请选择数据类型">
          <el-option label="机况数据" value="0" />
          <el-option label="生产数据" value="1" />
        </el-select>
      </el-form-item>
      <el-form-item label="同步方式" prop="syncMethod">
        <el-select v-model="formData.syncMethod" placeholder="请选择同步方式">
          <el-option label="下发指令" value="command" />
          <el-option label="上传文件" value="file" />
        </el-select>
      </el-form-item>
      <el-form-item
        v-if="formData.syncMethod === 'file'"
        label="文件"
        prop="file"
      >
        <el-upload
          class="upload-demo"
          action="#"
          :auto-upload="false"
          :on-change="handleFileChange"
        >
          <el-button type="primary">选择文件</el-button>
          <template #tip>
            <div class="el-upload__tip">请选择需要上传的文件</div>
          </template>
        </el-upload>
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
import { EquipmentSyncService } from "@/core/services/equipment-sync";
import type { EquipmentEntity } from "@/core/entities/equipment";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  data: {
    type: Object as PropType<EquipmentEntity>,
    required: true
  }
});

const emit = defineEmits(["update:visible", "success"]);

const dialogVisible = ref(false);
const formRef = ref();
const submitLoading = ref(false);

const formData = ref({
  syncType: "",
  syncMethod: "",
  file: null,
  equipmentId: ""
});

const formRules = {
  syncType: [{ required: true, message: "请选择同步类型", trigger: "change" }],
  syncMethod: [
    { required: true, message: "请选择同步方式", trigger: "change" }
  ],
  file: [{ required: true, message: "请上传文件", trigger: "change" }]
};

watch(
  () => props.visible,
  val => {
    dialogVisible.value = val;
    if (val && props.data) {
      formData.value.equipmentId = props.data.id;
    }
  }
);

watch(
  () => dialogVisible.value,
  val => {
    emit("update:visible", val);
  }
);

const handleFileChange = file => {
  formData.value.file = file.raw;
};

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

        const { syncType, file, equipmentId } = formData.value;
        await EquipmentSyncService.upload({
          syncType,
          file,
          equipmentId
        });

        ElMessage.success("同步成功");
        dialogVisible.value = false;
        emit("success");
        formRef.value?.resetFields();
      } catch (err) {
        ElMessage.error("同步失败");
      } finally {
        submitLoading.value = false;
      }
    }
  });
};
</script>

<style scoped>
.upload-demo {
  width: 360px;
}
</style>
