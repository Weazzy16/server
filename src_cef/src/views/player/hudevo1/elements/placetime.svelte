<script>
    import { TimeFormat } from 'api/moment'
    import { serverDateTime } from 'store/server'
    import { isInputToggled, isWaterMark, isPlayer, isHelp, isPhone } from 'store/hud'
    import { charWanted, charMoney, charBankMoney } from 'store/chars'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import { charFractionID, charOrganizationID } from 'store/chars'
    import CustomKey from './Key.svelte'
    import { executeClientAsyncToGroup } from "api/rage";
    import { format } from 'api/formatter'
    import { weatherName, formatTime, getWeatherIdToName } from '../phonenew/components/weather/data'

    import { writable } from 'svelte/store';

    export let SafeSone;

    let isWorld = true;
    window.hudStore.isWorld = (value) => isWorld = value;

    let direction = "NE";
    window.hudStore.direction = (value) => direction = value;

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
        UpdateMicrophoneIcon ();
    }
    let currentWeather = {
        // weatherId: 2,
        // hour: 12,
        // temp: 15
    }
    window.hudStore.currentWeather = (value) => currentWeather = value;

    // export const currentW = writable({})
    // window.currentWeather = {};

    // window.currentWeather.Set = (value) => {
    //     value = JSON.parse (value);
    //     currentW.set (value);
    // }
    
    // let weatherInfo = []
    // executeClientAsyncToGroup("getWeather").then((result) => {
    //     if (result && typeof result === "string") {
    //         weatherInfo = JSON.parse(result);

    //         currentWeather = weatherInfo[0];
    //     }
    // });

    let isMute = false;
    window.hudStore.isMute = (value) => {
        isMute = value;
        UpdateMicrophoneIcon ();
    }

    const UpdateMicrophoneIcon = () => {
        if (isMute) micIcon = "hud__icon-micmute";
        else if (microphone) micIcon = "hud__icon-micon";
        else micIcon = "hud__icon-micoff";
    }

    
    let polygon = 0;
    window.hudStore.polygon = (value) => polygon = value;

    let radio = 0;
    window.hudStore.radio = (value) => radio = value;

    
    let improvementTime = 0;
	window.hudStore.setImprovementTime = (value) => improvementTime = value;
    

    // const getOnlineName = (online) => {
    //     if(online <= 99)
    //         return "Низкий";
    //     else if(online <= 499)
    //         return "Средний";

    //     return "Высокий";
    // }
</script>
<div class="user-container">
    {#if improvementTime > 0}
		<div transition:fade={{ duration: 200 }} class="eternal__improvements">
			<img src={ImprovementIcon} alt="">
			<p>{ improvementTime }</p>
		</div>
	{/if}
    <div class="info-container">
        <div class="weather">
            <div class="weather__text-temperature">{currentWeather.temp}</div>
            <div class="weather__picture {getWeatherIdToName(currentWeather.weatherId, currentWeather.hour)}"></div>
            <div class="weather__text-name">{weatherName[getWeatherIdToName(currentWeather.weatherId, currentWeather.hour)]}</div>
        </div>
        <div class="row-block">
            <div class="time-container">
                <div class="main-time">
                    <div class="main-time__text-hours">{TimeFormat ($serverDateTime, "H:mm")}</div>
                    <div class="main-time__text-date">{TimeFormat ($serverDateTime, "DD.MM.YYYY")}</div>
                </div>
                <div class="time-container__text-weekday">{TimeFormat ($serverDateTime, "dddd")}</div>
            </div>
            {#if microphone != 0}
            <div class="voice">
                <img data-v-f24a0ae4="" src="https://cdn.majestic-files.com/img/main/hud/microphone.svg" alt="">
            </div>
            {/if}
        </div>
        <div class="place-info">
            <div class="region">{area}</div>
            <div class="street">{street}</div>
        </div>
        <div class="money">
            <div class="money-container">
                <div class="money-block cash_hud">
                    <span class="money-block__logo">
                        <svg data-v-7a91658e="" xmlns="http://www.w3.org/2000/svg" width="1.4814814814814814vh" height="1.4814814814814814vh" viewBox="0 0 16 16"><g id="money" transform="translate(0 0)"><path id="Контур_15474" data-name="Контур 15474" d="M.218,8.021a.333.333,0,0,0-.2.427l.209.576L1.823,7.429Z" fill="#fff"></path><path id="Контур_15475" data-name="Контур 15475" d="M1.727,13.141l.96,2.64a.33.33,0,0,0,.173.188A.335.335,0,0,0,3,16a.326.326,0,0,0,.115-.021l1.059-.391Z" fill="#fff"></path><path id="Контур_15476" data-name="Контур 15476" d="M15.98,10.887l-1.1-3.021L12.244,10.5l1.307-.481a.333.333,0,0,1,.231.625l-1.66.612a.333.333,0,0,1-.428-.2s0,0,0-.006L8.9,13.849l6.882-2.536A.332.332,0,0,0,15.98,10.887Z" fill="#fff"></path><path id="Контур_15477" data-name="Контур 15477" d="M15.9,5.431,10.569.1A.334.334,0,0,0,10.1.1l-10,10a.334.334,0,0,0,0,.471L5.431,15.9a.33.33,0,0,0,.235.1.335.335,0,0,0,.236-.1l10-10A.334.334,0,0,0,15.9,5.431ZM3.9,9.236,2.569,10.569A.334.334,0,0,1,2.1,10.1L3.431,8.765a.334.334,0,0,1,.472.471Zm5.756.423a1.258,1.258,0,0,1-.913.349,2.754,2.754,0,0,1-1.854-.885A3.054,3.054,0,0,1,6.059,7.7a1.393,1.393,0,0,1,.3-1.349A1.389,1.389,0,0,1,7.7,6.06a3.048,3.048,0,0,1,1.419.833C10.049,7.817,10.285,9.033,9.659,9.659ZM13.9,5.9,12.569,7.236a.334.334,0,0,1-.472-.471L13.43,5.431A.334.334,0,0,1,13.9,5.9Z" fill="#fff"></path></g></svg>
                    </span>
                    <span class="money-block__value">${format("money", $charMoney)}</span>
                </div>
                <div class="money-block bank_hud">
                    <span class="money-block__logo">
                        <svg xmlns="http://www.w3.org/2000/svg" width="17" height="16" viewBox="0 0 17 16">
                            <g id="deposit" transform="translate(0 -5.552)">
                                <g id="Group_461" data-name="Group 461" transform="translate(10.689 13.56)">
                                    <g id="Group_460" data-name="Group 460">
                                        <path id="Path_8507" data-name="Path 8507" d="M322.768,257.951a.434.434,0,0,0-.434.434v1.449a.434.434,0,0,0,.434.434h5.877v-2.317Zm.731,1.827a.669.669,0,1,1,.669-.669A.669.669,0,0,1,323.5,259.778Z" transform="translate(-322.334 -257.951)" fill="#fff"/>
                                    </g>
                                </g>
                                <g id="Group_463" data-name="Group 463" transform="translate(0 9.011)">
                                    <g id="Group_462" data-name="Group 462">
                                        <path id="Path_8508" data-name="Path 8508" d="M11.135,113.229H17v-2.356a1.411,1.411,0,0,0-1.443-1.373H1.443A1.411,1.411,0,0,0,0,110.873v9.8a1.411,1.411,0,0,0,1.443,1.373H15.557A1.411,1.411,0,0,0,17,120.669v-3.317H11.135a1.411,1.411,0,0,1-1.443-1.373V114.6A1.411,1.411,0,0,1,11.135,113.229Z" transform="translate(0 -109.5)" fill="#fff"/>
                                    </g>
                                </g>
                                <g id="Group_465" data-name="Group 465" transform="translate(7.254 5.552)">
                                    <g id="Group_464" data-name="Group 464" transform="translate(0 0)">
                                        <path id="Path_8509" data-name="Path 8509" d="M225.933,5.552,219.067,8H226.8Z" transform="translate(-219.067 -5.552)" fill="#fff"/>
                                    </g>
                                </g>
                            </g>
                        </svg>
                    </span>
                    <span class="money-block__value">${format("money", $charBankMoney)}</span>
                </div>
            </div>
        </div>
    </div>
    {#if greenZone}
        <div class="hudevo__greenzone" style="margin-bottom: -{SafeSone.y}px">
            <div class="hudevoicon-shield"></div>
            <div class="hudevo__greenzone_text"><b>Зеленая</b> зона</div>
        </div>
    {/if}
</div>