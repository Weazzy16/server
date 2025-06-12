<script>
    import { onMount } from 'svelte';
    import { tweened } from 'svelte/motion';
    import { cubicOut } from 'svelte/easing';
    import { TimeFormat } from 'api/moment';
    import { serverDateTime } from 'store/server';
    import { isInputToggled, isWaterMark, isPlayer, isHelp, isPhone } from 'store/hud';
    import keys from 'store/keys';
    import keysName from 'json/keys.js';
    import bankimg from './assets/img/Subtract.svg';
    import walletimg from './assets/img/Union.svg';
    import { charFractionID, charOrganizationID } from 'store/chars';
    import CustomKey from './Key.svelte';
    import { itemsInfo } from 'json/itemsInfo.js';
    import { serverDonatMultiplier } from 'store/server';
    import { charWanted, charMoney, charBankMoney } from 'store/chars';
    import { fly } from 'svelte/transition';
    import { format } from 'api/formatter';
    import { translateText } from 'lang';
    import { getQuest } from 'json/quests/quests.js';
    import router from 'router';
    import { executeClient } from 'api/rage';
    import { executeClientAsyncToGroup } from "api/rage";
    import { weatherName, getWeatherIdToName } from './data.js';
    import { get } from 'svelte/store';


    let weatherIcon = 'sunny';
    let weatherDescription = 'Солнечно';
    let temperature = '20';

    const weatherMap = {
        0: 'sunny',
        1: 'sunny',
        2: 'cloud',
        3: 'cloud',
        4: 'fog',
        5: 'overcast',
        6: 'rain',
        7: 'thunder',
        8: 'cloud',
        10: 'snowlight',
        11: 'snow',
        12: 'blizzard',
        13: 'xmas'
    };

    window.hudStore.weatherId = (weatherId) => {
        const hour = new Date(get(serverDateTime)).getHours();
        const baseIcon = weatherMap[weatherId] || 'sunny';
        const isNight = hour >= 0 && hour < 6;

        if (isNight && ['sunny', 'cloud', 'overcast'].includes(baseIcon)) {
            weatherIcon = 'night';
        } else {
            weatherIcon = baseIcon;
        }

        weatherDescription = weatherName[weatherIcon] || 'Погода';

        console.log(`Weather ID changed: ${weatherId}, hour: ${hour}, icon: ${weatherIcon}, desc: ${weatherDescription}`);
    };

    window.hudStore.weatherTemp = (temp) => {
        temperature = temp;
        console.log(`Temperature changed: ${temp}`);
    };

    window.hudStore.currentWeather = (weatherId, temp) => {
    // Поддержка передачи объекта вместо отдельных параметров
    if (typeof weatherId === 'object') {
        const data = weatherId;
        weatherId = data.weatherId;
        temp = data.temp;
    }

    const hour = new Date(serverDateTime).getHours();
    weatherIcon = getWeatherIdToName(weatherId, hour);
    weatherDescription = weatherName[weatherIcon];
    temperature = temp;

    console.log(`Weather ID changed: ${weatherId}, hour: ${hour}, icon: ${weatherIcon}, desc: ${weatherDescription}`);
    console.log(`Temperature changed: ${temperature}`);
};


    export let SafeSone;

    const money = tweened(0, {
        duration: 700,
        easing: cubicOut
    });

    const bank = tweened(0, {
        duration: 700,
        easing: cubicOut
    });

    let moneyChangeDirection = null;
    let bankChangeDirection = null;

    let previousMoney = 0;
    let previousBank = 0;

    let isWorld = true;
    window.hudStore.isWorld = (value) => isWorld = value;

    let greenZone = false;
    window.hudStore.greenZone = (value) => greenZone = value;

    let street = "";
    window.hudStore.street = (value) => street = value;

    let area = "";
    window.hudStore.area = (value) => area = value;

    let micIcon = "hud__icon-micoff";

    let microphone = 0;
    window.hudStore.microphone = (value) => {
        microphone = value;
        UpdateMicrophoneIcon();
    }

    let isMute = false;
    window.hudStore.isMute = (value) => {
        isMute = value;
        UpdateMicrophoneIcon();
    }

    const UpdateMicrophoneIcon = () => {
    if (isMute) micIcon = "hud__icon-micmute";
    else if (microphone) micIcon = "hud__icon-custommic";  // используем кастомный значок
    else micIcon = "hud__icon-micoff";
}



    let polygon = 0;
    window.hudStore.polygon = (value) => polygon = value;

    let radio = 0;
    window.hudStore.radio = (value) => radio = value;

    let serverPlayerId = 0;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let serverOnline = 0;
    window.serverStore.serverOnline = (value) => serverOnline = value;

    const getOnlineName = (online) => {
        if (online <= 99)
            return "Низкий";
        else if (online <= 499)
            return "Средний";

        return "Высокий";
    }

    const formatNumberWithSpaces = (number) => {
        if (typeof number !== 'number') return '0';
        return number.toLocaleString('ru-RU').replace(/,/g, ' ');
    }

    onMount(() => {
        charMoney.subscribe(value => {
            const numericValue = Number(value);
            if (isNaN(numericValue)) return;

            if (numericValue > previousMoney) moneyChangeDirection = 'increase';
            else if (numericValue < previousMoney) moneyChangeDirection = 'decrease';
            else moneyChangeDirection = null;

            money.set(numericValue);
            previousMoney = numericValue;

            if (moneyChangeDirection) {
                setTimeout(() => {
                    moneyChangeDirection = null;
                }, 700);
            }
        });

        charBankMoney.subscribe(value => {
            const numericValue = Number(value);
            if (isNaN(numericValue)) return;

            if (numericValue > previousBank) bankChangeDirection = 'increase';
            else if (numericValue < previousBank) bankChangeDirection = 'decrease';
            else bankChangeDirection = null;

            bank.set(numericValue);
            previousBank = numericValue;

            if (bankChangeDirection) {
                setTimeout(() => {
                    bankChangeDirection = null;
                }, 700);
            }
        });
    });
</script>

<style>
    .money, .bank {
        transition: transform 0.2s, color 2.2s;
    }
    .increase {
        transform: scale(1.02);
        color: #6fec74;
    }
    .decrease {
        transform: scale(1.02);
        color: #f44336;
    }
    




</style>

<div class="hudevo__placetime">
    <div class="placetime__buttonsw">
        <div class="playerinfo__box">
            <CustomKey bottom={true} keyCode={$keys[36]} nonactive={isMute || $isInputToggled}>
                <span class="placetime__icon {micIcon}" class:active={microphone} class:polygon={polygon} class:mute={isMute}></span>
                
            </CustomKey>
            
        </div>
    </div>

    <div class="hud_left_bottom">
        <div class="hud_weather_block">
            <span class="weather_temp">+ {temperature}</span>
            <img class="weather_icon" src={`http://cdn.piecerp.ru/cloud/weather/${weatherIcon}.svg`} alt={weatherDescription}>
            <span class="weather_desc">{weatherDescription}</span>
        </div>


        <div class="leftbottom_timeblock">
            <div class="leftbottom_timeblock_top">
                <span>{TimeFormat($serverDateTime, "H:mm")}</span>
                <a>{TimeFormat($serverDateTime, "DD.MM.YYYY")}</a>
            </div>
            <span class="timeblock_top_day"> {TimeFormat($serverDateTime, 'dddd')[0].toUpperCase() + TimeFormat($serverDateTime, 'dddd').slice(1)}
            </span>
        </div>
        <div class="streetblock">
        <div class="leftbottom_streetblock">{area}</div>
        <div class="leftbottom_streetblocks">{street}</div>
    </div>
        <div class="leftbottom_moneyblock">
            <div class="leftbottom_moneyblock_money">
                <img src={walletimg} alt="Wallet">
                <span>$ </span>
                <span class="money {moneyChangeDirection}">
                    {formatNumberWithSpaces(Math.round($money))}
                </span>
            </div>
            <div class="leftbottom_moneyblock_bank">
                <img src={bankimg} alt="Bank">
                <span>$ </span>
                <span class="bank {bankChangeDirection}">
                    {formatNumberWithSpaces(Math.round($bank))}
                </span>
            </div>
        </div>
    </div>
</div>
