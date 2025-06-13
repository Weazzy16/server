<script>
    import { translateText } from 'lang'
    import './assets/sass/main.sass'
    import { charGender } from 'store/chars';
    import { executeClient } from 'api/rage'
    import { ItemId } from 'json/itemsInfo.js'
    export let visible;

    const eventItem = ItemId.Giftcoin;

    //gender - true - мужик / false - женский
    let items = [ 
        {title: 'Unique pants exchange', descr: 'Get unique Christmas pants!', price: 99999, gender: true},
        {title: 'Exchange for unique top', descr: 'Get a random birth from outerwear', price: 650, gender: true},
        {title: 'Exchange for a unique accessory', descr: 'Get random louds on their backs', price: 1000, gender: true},

        {title: 'Unique pants exchange', descr: 'Get unique Christmas pants', price: 650, gender: false},
        {title: 'Exchange for unique top', descr: 'Get a random birth from outerwear', price: 650, gender: false},
        {title: 'Exchange for a unique accessory', descr: 'Get random louds on their backs', price: 1000, gender: false},
    
        {title: 'Snowmobile exchange!', descr: 'You can get a unique transport - a snowmobile!', price: 1500, gender: "all"},

    ];

    let valueEC = window.getItemToCount (eventItem);

    let useVisible = visible;
    $: {
        if (visible !== useVisible) {
            useVisible = visible;
            valueEC = window.getItemToCount (eventItem);
        }
    }
    const Bool = (text) => {
        return String(text).toLowerCase() === "true";
    }

    const onBuyItem = (index) => {
        if (items [index].price > window.getItemToCount (eventItem)) {
            valueEC = window.getItemToCount (eventItem);
            window.notificationAdd(4, 9, translateText('player2', 'У Вас нет столько Coins!'), 3000);
            return;
        }
        executeClient ("client.events.buyItem", index);
    }
    
    const UpdateSlot = (json) => {
        json = JSON.parse(json)
        if (json.ItemId == eventItem) {
            valueEC = window.getItemToCount (eventItem);
        }
    }

    window.events.addEvent("cef.events.UpdateSlot", UpdateSlot);

    import { onDestroy } from 'svelte'
    onDestroy(() => {
        // Инициализация инвентаря игрока
        window.events.removeEvent("cef.events.UpdateSlot", UpdateSlot);
    });
</script>


<div class="event">
    <!-- <div class="event__bottom"></div> -->
    <div class="m">
        <div class="event-logo"></div>
         <div class="event-snow"/> 
    </div>
    <div class="event-main">
        <div class="event-header">
        <div class="event-icon-sun"/>
        <div><span>{translateText('player2', 'Баланс')} :</span><span class="yellow">{valueEC} Coin's </span></div>
        <div class="event-icon-sun"/>
        </div>
        <ul class="event-list">
            <div class="event__main_title">Boring? It's time to collect 160 boxes from Santa! Go in search of the entire staff and exchange the gifts received for steep prizes in this menu, or to Redbucks at the NPC Santa.</div>
            {#each items as item, index}
                {#if item.gender === "all" || Bool($charGender) === item.gender}
                <li>
                    <div class="event-list-box">
                        <span class="title">
                            {item.title}
                        </span>
                        <span class="descr">
                            {item.descr}
                        </span>
                    </div>
                    <div class="event-icon-gift"/>
                    <div class="btn green" on:keypress={() => {}} on:click={() => onBuyItem (index)}>{item.price} Coin's</div>
                </li>
                {/if}
            {/each}
        </ul>
    </div>
</div>
