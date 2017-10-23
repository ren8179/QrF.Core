<template>
	<el-col :xs="xs" :sm="sm" :md="md">
    <el-form-item :label="label+'：'" :prop="code">
      <slot></slot>
    </el-form-item>
  </el-col>
</template>

<script>
export default {
  name: 'cl',
  props: {
    label: {
      type: String,
      default: null
    },
    code: {
      type: String,
      default: null
    },
    xs: {
      type: Number,
      default: 12
    },
    sm: {
      type: Number,
      default: 8
    },
    md: {
      type: Number,
      default: 6
    }
  }
}
</script>
