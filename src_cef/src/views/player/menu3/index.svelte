<script>
    import { translateText } from 'lang'
    import './main.sass';
    import './main.css';
    import './fonts/inv/style.css';
    import './fonts/items/style.css';
    import './fonts/gamemenu/style.css';
    import './fonts/Gilroy/stylesheet.css';
    import './fonts/SFPro/stylesheet.css';

    import { serverDateTime, isEvent } from 'store/server'
    import { charData } from 'store/chars';
    import { accountRedbucks } from 'store/account'
    import { format } from 'api/formatter'
    import { executeClient } from 'api/rage'
    import { otherStatsData } from 'store/account'

    export let visible = false;
    
    let useVisible = -1;

    $: {
        if (useVisible != visible) {
            if (visible && $otherStatsData.Name/* && $otherStatsData.UUID !== selectCharData.UUID*/) {
                selectCharData = $otherStatsData;
            } else if (visible && !$otherStatsData.Name && selectCharData !== $charData) {
                selectCharData = $charData;
            } else if (!visible && $otherStatsData.Name) {
                selectCharData = $charData;
                window.accountStore.otherStatsData ('{}');
            }
            useVisible = visible;
        }
    }
    
    let selectCharData = $charData; 
    let selectView = "Inventory";
    let timerView = "Inventory";

    import Inventory from "./elements/inventory/inventory.svelte";
    import Stats from "./elements/stats.svelte";
    import Settings from "./elements/settings/index.svelte";
    import Quests from "./elements/quests/index.svelte";
    import Events from "./elements/events.svelte";
    import Support from "./elements/support/index.svelte";
    import RewardsList from "./elements/rewardslist/index.svelte";
    import Table from "./elements/fractions/index.svelte";


    const Views = {
        Stats,
        Settings,
        Quests,
        Events,
        Support,
        RewardsList,
        Fractions: Table,
        Organization: Table,
    }
 
    
    window.gameMenuView = (wiew) => {
        selectView = wiew;
        timerView = wiew;
    }
    const defaultSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList", "Fractions", "Organization", "Events"];
    let _pagesSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList"];
    let PagesSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList"];

    const updatePage = (name, value) => {

        const index = _pagesSorted.indexOf(name)

        if (index !== -1 && !value)
            _pagesSorted.splice(index, 1);
        else if (index === -1 && value)
            _pagesSorted.push(name)

        let sorted = [];

        defaultSorted.forEach((value) => {
            if (_pagesSorted.includes (value))
                sorted.push(value)
        })

        PagesSorted = sorted;
    }

    import { charFractionID, charOrganizationID } from 'store/chars'


    isEvent.subscribe(value => {
        updatePage ("Events", value);
    });
    charFractionID.subscribe(value => {
        updatePage ("Fractions", value > 0);
    });
    charOrganizationID.subscribe(value => {
        updatePage ("Organization", value > 0);
    });


    let UseVisible = visible;
    let TimeQE = 0;


    import { onInputBlur } from './elements/fractions/data'

    $: {
        if (UseVisible != visible) {
            UseVisible = visible;
            TimeQE = new Date().getTime() + 250;
            if (!visible) {
                selectView = "Inventory";
                timerView = "Inventory";
                onInputBlur ();
            }
        }
    }

    let isFocusInput = false;

    window.onFocusInput = (value) => isFocusInput = value;

    let timerId = null;

    const setPage = () => {
        if (timerId !== null)
            clearTimeout(timerId);
            
        timerId = setTimeout(() => {
            selectView = timerView;
            timerId = null;
        }, 200)
    }

    function onClickQ() {
        let index = PagesSorted.findIndex (p => p === selectView)
        
        if(--index >= 0) {
            selectView = PagesSorted [index];
            //setPage ();
        }
    }

    function onClickE() {
        let index = PagesSorted.findIndex (p => p === selectView)

        if (++index < PagesSorted.length) {
            selectView = PagesSorted [index];
            //setPage ();
        }
    }

    const onOpenBattlePass = () => {
        executeClient ("client.battlepass.open");
    }
    const onOpenFraction = () => {
        executeClient ("client.fraction.open");
    }
    
    const onKeyUp = (event) => {
        if (isFocusInput)
            return;

        if (!visible) return;
        else if (TimeQE > new Date().getTime()) return;

        const { keyCode } = event;
        
        if(keyCode == 81) {
            onClickQ ();
        } else if(keyCode == 69) { 
            onClickE ();
        }
    }
</script>
<svelte:window on:keyup={onKeyUp} />
<div class="playermenuhax" style="display: {visible ? 'flex' : 'none'}">
    <div class="headerpl">
        <div class="topheaderp">
            <div class="Usernamep">
                {#if selectView === "Inventory"}
                    <h1>Инвентарь</h1>
                    {:else}
                    <h1>Меню Персонажа</h1>
                {/if}
                <p><span class="lefttpl"></span>{selectCharData.Login}<span class="rightpl"></span></p>
            </div>
            <div class="navmenupl">
                <div class="itempl" class:active={selectView === "Inventory"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Inventory")}>
                    <p>{translateText('player1', 'Инвентарь')}</p>
                </div>
                <!-- <div class="itempl" class:active={selectView === "Stats"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Stats")}>
                    <p>{translateText('player1', 'Статистика')}</p>
                </div>
                <div class="itempl"  class:active={selectView === "Settings"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Settings")}>
                    <p>{translateText('player1', 'Настройки')}</p>
                </div>
                <div class="itempl" class:active={selectView === "Quests"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Quests")}>
                    <p>{translateText('player1', 'Квесты')}</p>
                </div> -->
                {#if PagesSorted.includes("Fractions")}
                    <div class="itempl" class:active={selectView === "Fractions"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Fractions")}>
                        <p>{translateText('player1', 'Фракция')}</p>
                    </div>
                {/if}
                {#if PagesSorted.includes("Organization")}
                    <div class="itempl" class:active={selectView === "Organization"} on:keypress={() => {}} on:click={() => window.gameMenuView ("Organization")}>
                        <p>{translateText('player1', 'Организация')}</p>
                    </div>
                {/if}
                <div class="itempl" on:keypress={() => {}} on:click={onOpenBattlePass}>
                    <p>Боевой Пропуск</p>
                </div>
            </div>
            <div class="balancuser">
                <h1>Баланс:</h1>
                <p>{format("money", $accountRedbucks)}</p>
            </div>
            <div class="logo"></div>
        </div>
        <div class="lineheader"></div>
    </div>
    <div class="selectermenu">
        <div id="box-menu">
            <Inventory visible={selectView === "Inventory"} />
            <svelte:component this={Views[selectView]} {visible} {selectView} />
        </div>
    </div>
</div>