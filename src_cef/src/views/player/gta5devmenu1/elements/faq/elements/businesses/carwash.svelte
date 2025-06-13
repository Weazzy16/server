<script>
    export let onWayPoint;
</script>
<div class="headrules">
    <h1>Автомойка</h1>
</div>


<div class="rulesblockbg">
    <div class="text">
        <h1>Автомойка</h1>
        <p><span style="font-weight: 400;"><strong>Ваша машина совсем утонула в грязи?</strong> Тогда быстрее едьте в одну из ближайших автомоек.</span></p>
        <p><span style="font-weight: 400;">Чтобы воспользоваться услугами автомойки, необходимо подъехать на вашем транспорте к "<strong>Маркеру</strong>", который расположен в автомойке, <strong>заглушить ваш транспорт</strong> и нажать "<strong>E</strong>", после этого подтвердить, что вы хотите воспользоваться данной услугой. После подтверждения транспорт снова будет чист.</span></p>
        <!-- <p><span style="font-weight: 400;"><strong>Стоимость</strong> одной мойки транспорта - от 95$ до 175$.</span></p> -->

        <h1><div class="btn" on:click={onWayPoint}>Показать ближайшую<span class="icon map"/></div></h1>
    </div>
</div>