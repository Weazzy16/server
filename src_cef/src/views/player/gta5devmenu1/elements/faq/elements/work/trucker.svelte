<script>
    export let onWayPoint;
</script>

<div class="headrules">
    <h1>Дальнобойщик</h1>
</div>
<div class="rulesblockbg">
    <div class="text">
        <h1>Дальнобойщик</h1>
        <p><span style="font-weight: 400;">Если ваш организм привык к долгому отсутствию сна и вы также можете сохранять концентрацию и следить за дорогой, то данная работа для вас.</span></p>
        <p><span style="font-weight: 400;">После трудоустройства необходимо выйти из мэрии и использовать навигатор в телефоне, выбрать &laquo;<strong>GPS &gt; Работы &gt; Стоянка дальнобойщиков</strong>&raquo;.</span></p>
        <p><span style="font-weight: 400;">После прибытия на стоянку, следует пройти к свободному автомобилю и сесть в него, после этого будет предложено арендовать его. Чтобы открыть меню с заказами, нужно прописать команду &ldquo;<strong>/orders</strong>&rdquo; и выбрать понравившийся заказ, который нужно будет забрать со склада и отвезти заказчику.</span></p>
        <p><span style="font-weight: 400;"><strong>Требования</strong>: права категории &ldquo;<strong>C</strong>&rdquo;<br /></span><span style="font-weight: 400;"><strong>Зарплата</strong>: от 800$ до 3.500$</span></p>
        <p><span style="font-weight: 400;"><strong>Команды</strong>:</span></p>
        <p><span style="font-weight: 400;"><strong>/orders</strong> - открыть меню с выбором заказа (работает только на точке загрузки товаров)<br />На точках загрузки товаров были добавлены метки взаимодействия, которые так же открывают меню заказов.<br /></span><strong>/t</strong> - рация дальнобойщиков.<br /><strong>/findtrailer</strong> - если вы потеряли трейлер, данная команда поможет вам его найти.<br /><strong>/mypoint</strong> - с помощью данной команды, вы сможете узнать куда вам необходимо доставить груз.</p>
        <!-- <p><br /><br /></p> -->
          <h1 on:click={onWayPoint}>Показать на карте </h1>
    </div>
</div>