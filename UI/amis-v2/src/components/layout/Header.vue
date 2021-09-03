<template>
  <div class="AMIS-header dis-flex justify-content-between h-48">
    <div class="header-left dis-flex">
      <div class="logo three-striped-logo m-x-10 pointer"></div>
      <div class="header-branch pointer">
        <div class="branch-selected">
          CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
        </div>
        <div class="icon branch-icon m-x-10"></div>
      </div>
    </div>

    <div class="header-right dis-flex pointer">
      <div class="logo notify-logo m-x-12"></div>
      <div class="user-container dis-flex p-r-20">
        <div class="avatar m-x-12"></div>
        <div class="user-name p-r-8">Đỗ Mạnh Hà</div>
        <div class="icon user-branch-icon p-r-16"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.AMIS-header {
  position: absolute;
  top: 0;
  left: 180px;
  right: 0;
  display: flex;
  align-items: center;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}

.header-branch {
  display: flex;
  align-items: center;
}

.branch-selected {
  font-family: MISANotosans-SemiBold;
  font-size: 14px !important;
}

.user-container {
  align-items: center;
}

.avatar {
  width: 24px;
  height: 24px;
  background-image: url("../../assets/img/default-avatar.jpg");
  background-size: contain;
  background-position: center;
  background-repeat: no-repeat;
  border-radius: 50%;
}

.user-container .user-name {
  font-family: MISANotosans-SemiBold;
  font-size: 14px;
}
</style>