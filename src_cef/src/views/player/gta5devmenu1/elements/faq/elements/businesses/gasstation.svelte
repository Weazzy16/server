<script>
    export let onWayPoint;
</script>
<div class="headrules">
    <h1>АЗС</h1>
</div>


<div class="rulesblockbg">
    <div class="text">
        <h1>Автомобильная Заправочная Станция</h1>
        <p><span style="font-weight: 400;">Не хватает бензина чтобы проехать вокруг штата? А канистру купить забыли? Скорее езжайте на ближайшую АЗС!</span></p>
        <p><span style="font-weight: 400;"><strong>При подъезде</strong> на заправочную станцию необходимо <strong>заглушить транспортное средство</strong> и нажать клавишу "<strong>E</strong>", после чего нужно <strong>ввести необходимое</strong> количество топлива.</span></p>
        <p><span style="font-weight: 400;">Также вы <strong>можете использовать следующие клавиши</strong>:&nbsp;</span></p>
        <p><span style="font-weight: 400;"><strong>FULL</strong> - Заправит ваш автомобиль до полного бака;&nbsp;<br /></span><strong>GOV</strong> - Бесплатно заправит ваш автомобиль, данная клавиша доступна только сотрудникам государственных организаций.</p>
        <p></p>

        <h1><div class="btn" on:click={onWayPoint}>Показать ближайший<span class="icon map"/></div></h1>
    </div>
</div>