<script>
    import { itemsInfo } from 'json/itemsInfo.js'
    import { serverDonatMultiplier } from 'store/server'
    import { charUUID, charWanted, charMoney, charBankMoney } from 'store/chars'
    import { isWaterMark, isPlayer } from 'store/hud'
    import { fly } from 'svelte/transition';
    import { format } from 'api/formatter'
    import { CountUp } from 'countup.js';
    import { selected, reportsData, text } from '../../reports/index'
    export let SafeSone;
    import { writable } from 'svelte/store';
    import Quest from './quest.svelte'

    let reports = []

    let userData = {
        targetMoney: 0,
        changeMoney: 0,
        timerIdMoney: 0,
        Money: 0,
        targetBank: 0,
        changeBank: 0,
        timerIdBank: 0,
        Bank: 0,
    };

    reportsData.subscribe(value => {
		reports = value;
    });

    import { onMount } from 'svelte';
    onMount(async () => {
        //bar.animate(1.0);

        charMoney.subscribe(value => {
            if (userData.Money !== value) {
                CounterUpdate ("Money", value);
            }
        });
        charBankMoney.subscribe(value => {
            if (userData.Bank !== value) {
                CounterUpdate ("Bank", value);
            }
        });
    });

    const CounterUpdate = (args, value) => {
        userData["change" + args] = userData[args] > value ? (0 - (userData[args] - value)) : (value - userData[args]);
        userData[args] = value;

        if (!userData["target" + args]) {
            userData["target" + args] = new CountUp("target" + args, value);
            userData["target" + args].start(() => {
                userData["change" + args] = 0;
            });
        }
        else
            userData["target" + args].update(value);
    }

    let serverName = "";
    window.setServerName = (name) => serverName = name;

    let isRotate = false;
    
    const secretFunction = () => {
        isRotate = !isRotate;

    }
    let serverOnline = 0;
    window.serverStore.serverOnline = (value) => serverOnline = value;

    let serverPlayerId = 0;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let weaponItemId = 0;
    window.hudStore.weaponItemId = (value) => weaponItemId = value;


    let clipSize = 0;
    window.hudStore.clipSize = (value) => clipSize = value;

    let ammo = 0;
    window.hudStore.ammo = (value) => ammo = value;

    let isShow = false;

    serverDonatMultiplier.subscribe(value => {
        if (value > 1) {
            isShow = true;

            setTimeout(() => {
                isShow = false;
            }, 1000 * 30);
        }
    });

    export const isAdmin = writable(false);
    window.hudStore.isAdmin = (value) => isAdmin.set(value)
</script>
<div class="hud-shadow"></div>
<div class="topBox">
    <div class="online__logo">
        {#if $isAdmin == true}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        <div class="report-info">
            <div class="report-info-container">
                <div class="report-info__title">Репорты:</div>
                <div class="reports-amount">{reports.length}</div>
            </div>
        </div>
        {/if}
        <!-- <img src="https://cdn.majestic-files.com/img/main/hud/logotype.svg" alt=""> -->
        <img src="http://cdn.piecerp.ru/src/views/player/newhud/images/logo.svg" alt="">
    </div>
    <div class="server-info">
        <div class="online-text">
            <span class="online-text__title">Online:</span>
            <div class="online-text__text">{serverOnline}</div>
        </div>
        <div class="server-id">
            <div class="server-id__text">RU1</div>
        </div>
    </div>
    <div class="player-id">
        <div class="player-id__text-1">ID: {serverPlayerId} |</div>
        <div class="player-id__text-2">#{$charUUID}</div>
    </div>
    {#if ammo > 0}
       <div class="bullets-block">
            <div class="bullets-block__text"><span>{ammo}</span></div>
            <img src="https://cdn.majestic-files.com/img/main/hud/bullets.svg" class="bullets-block__picture" alt="">
       </div> 
    {/if}
    {#if $charWanted > 0}
    <div class="wanted">
        <svg class={ 'wanted-star ' + ($charWanted >= 5 ? 'active' : '') } width="1.8518518518518516vh" height="1.8518518518518516vh" viewBox="0 0 20 20"><defs><clipPath><rect width="20" height="20" transform="translate(1883 155)" fill="#fff"></rect></clipPath></defs><g transform="translate(-1883 -155)"><g transform="translate(1883 155)"><path d="M10,15.773,16.18,19.5l-1.635-7.029L20,7.744l-7.191-.617L10,.5,7.191,7.127,0,7.744l5.455,4.727L3.82,19.5Z" fill="#fff"></path></g></g></svg>
        <svg class={ 'wanted-star ' + ($charWanted >= 4 ? 'active' : '') } width="1.8518518518518516vh" height="1.8518518518518516vh" viewBox="0 0 20 20"><defs><clipPath><rect width="20" height="20" transform="translate(1883 155)" fill="#fff"></rect></clipPath></defs><g transform="translate(-1883 -155)"><g transform="translate(1883 155)"><path d="M10,15.773,16.18,19.5l-1.635-7.029L20,7.744l-7.191-.617L10,.5,7.191,7.127,0,7.744l5.455,4.727L3.82,19.5Z" fill="#fff"></path></g></g></svg>
        <svg class={ 'wanted-star ' + ($charWanted >= 3 ? 'active' : '') } width="1.8518518518518516vh" height="1.8518518518518516vh" viewBox="0 0 20 20"><defs><clipPath><rect width="20" height="20" transform="translate(1883 155)" fill="#fff"></rect></clipPath></defs><g transform="translate(-1883 -155)"><g transform="translate(1883 155)"><path d="M10,15.773,16.18,19.5l-1.635-7.029L20,7.744l-7.191-.617L10,.5,7.191,7.127,0,7.744l5.455,4.727L3.82,19.5Z" fill="#fff"></path></g></g></svg>
        <svg class={ 'wanted-star ' + ($charWanted >= 2 ? 'active' : '') } width="1.8518518518518516vh" height="1.8518518518518516vh" viewBox="0 0 20 20"><defs><clipPath><rect width="20" height="20" transform="translate(1883 155)" fill="#fff"></rect></clipPath></defs><g transform="translate(-1883 -155)"><g transform="translate(1883 155)"><path d="M10,15.773,16.18,19.5l-1.635-7.029L20,7.744l-7.191-.617L10,.5,7.191,7.127,0,7.744l5.455,4.727L3.82,19.5Z" fill="#fff"></path></g></g></svg>
        <svg class={ 'wanted-star ' + ($charWanted >= 1 ? 'active' : '') } width="1.8518518518518516vh" height="1.8518518518518516vh" viewBox="0 0 20 20"><defs><clipPath><rect width="20" height="20" transform="translate(1883 155)" fill="#fff"></rect></clipPath></defs><g transform="translate(-1883 -155)"><g transform="translate(1883 155)"><path d="M10,15.773,16.18,19.5l-1.635-7.029L20,7.744l-7.191-.617L10,.5,7.191,7.127,0,7.744l5.455,4.727L3.82,19.5Z" fill="#fff"></path></g></g></svg>
    </div>
    {/if}
    <Quest />
</div>