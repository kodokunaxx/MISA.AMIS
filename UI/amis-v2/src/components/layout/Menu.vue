<template>
  <div class="AMIS-menu">
    <div class="logo-container h-48">
      <div class="logo option-logo m-r-10 pointer"></div>
      <div class="logo-home pointer"></div>
    </div>
    <div class="menu-item-list">
      <div
        class="menu-item h-42 p-x-14 pointer"
        v-for="(item, i) in items"
        :key="i"
        :class="i === index ? 'selected' : ''"
        v-on:click="
          index = i;
          redirect();
        "
      >
        <div class="logo m-r-8" v-bind:class="item.icon"></div>
        <div class="item-text">{{ item.title }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  created() {},
  data() {
    return {
      index: 1,
      items: [
        { icon: "dashboard-logo", title: "Tổng quan" },
        { icon: "cash-logo", title: "Tiền mặt" },
        { icon: "bank-logo", title: "Tiền gửi" },
        { icon: "purchase-logo", title: "Mua hàng" },
        { icon: "sale-logo", title: "Bán hàng" },
        { icon: "invoice-logo", title: "Quản lý hóa đơn" },
        { icon: "stock-logo", title: "Kho" },
        { icon: "tools-logo", title: "Công cụ dụng cụ" },
        { icon: "fixed-assets-logo", title: "Tài sản cố định" },
        { icon: "tax-logo", title: "Thuế" },
        { icon: "price-logo", title: "Giá thành" },
        { icon: "general-logo", title: "Tổng hợp" },
        { icon: "budget-logo", title: "Ngân sách" },
        { icon: "report-logo", title: "Báo cáo" },
        { icon: "finance-logo", title: "Phân tích tài chính" },
      ],
    };
  },
  methods: {
    /**
     * Chuyển hướng đến dashboard khi click menu item
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    redirect() {
      this.$router.push("dashboard").catch((error) => {
        if (error.name != "NavigationDuplicated") {
          throw error;
        }
      });
    },
  },
};
</script>

<style>
.AMIS-menu {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  width: 180px;
  height: 100vh;
  background-color: #38393d;
}

.logo-container {
  display: flex;
  padding: 24px;
  align-items: center;
}

.logo-home {
  width: 96px;
  height: 32px;
  background-image: url("../../assets/img/Logo_Module_TiengViet_White.66947422.svg");
  background-size: contain;
  background-position: center;
  background-repeat: no-repeat;
}

.menu-item-list {
  padding-top: 14px;
  max-height: calc(100vh - 48px);
  overflow: auto;
}

::-webkit-scrollbar {
  width: 6px;
}

::-webkit-scrollbar-track {
  background: #f1f1f1;
}

::-webkit-scrollbar-thumb {
  background: #b8bcc3;
}

::-webkit-scrollbar-thumb:hover {
  background: #888888;
}
.menu-item {
  display: flex;
  align-items: center;
  color: #fbfbfc;
}

.menu-item:hover {
  background-color: #5d5d61;
}

.menu-item.selected {
  background-color: #5d5d61;
  border-left: 6px solid #2ca01c;
}
</style>