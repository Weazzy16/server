<script>
    import { translateText } from 'lang'
    export let popupData;
    import { onDestroy } from 'svelte';
    import {executeClient, executeClientAsync} from 'api/rage'
    import './main.sass';
    import './main.css';

    let caseData = {}
    const getData = () => {
        executeClientAsync("donate.roulette.getCaseOne").then((result) => {
            if (result && typeof result === "string") {
                caseData = JSON.parse(result);
                selectCaseToItems = caseData.items;
                casesData = GetRouletteData ()
                isLoad = true;
            }
        });
    }
    
    executeClient ("client.donate.roulette.loadCase", popupData);

    let isLoad = false;

    import { addListernEvent } from 'api/functions';
    addListernEvent ("donate.roulette.initCase", getData)
    
    import PoputWin from './popupprise.svelte'
    let dataPopup;
    let isPopup;

    const maxCount = 8;

    const SetPopup = (toggled = false, data = null) => {
        dataPopup = data;
        isPopup = toggled;
    } 

    let antiFlud = 0;

    const getRndInteger = (min, max) => {
        return Math.floor(Math.random() * (max - min + 1) ) + min;
    }

    const GetRouletteData = () => {
        let _casesData = [];
        for(let i = 0; i < maxCount; i++) {
            let newItems = [];
            const sCase = caseData.items;
            let randToIndex;

            for (let i = 0; i < 50; i++) {
                randToIndex = getRndInteger (0, sCase.length - 1);
                newItems = [
                    ...newItems,
                    sCase[randToIndex]
                ]
            }

            _casesData.push({
                randomBlocks: newItems,
                startRandomBlocks: newItems.slice(0, 9),
                winBlock: {},
                carousel: 0,
                carouselStart: 0,
                fixСarousel: true,
                IntervalId: null,
            });
        }
        return _casesData;
    }

    let currentCount = 1;
    let
        casesData = {},
        toggledFast = false,
        selectCaseToItems = [];

    let isConfirm = false;
    const Confirm = (data) => {
        if (isConfirm)
            return;

        isConfirm = true;
        data.forEach((caseItem, caseindex) => {
            const elemWidth = document.querySelector(`#popuponate__roulette .newdonate__roulette-main:nth-child(${caseindex + 1}) .newdonate__roulette-element:first-child`);
            let newItems = casesData [caseindex].startRandomBlocks;
            let randToIndex;
            for (let index = newItems.length; index < 50; index++) {
                
                if (index === caseItem.Index) {
                    newItems = [
                        ...newItems,
                        selectCaseToItems[caseItem.ItemIndex]
                    ];
                } else {
                    randToIndex = getRndInteger (0, selectCaseToItems.length - 1);

                    newItems = [
                        ...newItems,
                        selectCaseToItems[randToIndex]
                    ];
                }                    
            }

            const randomCarousel = Math.round(getRndInteger (0 - (elemWidth.clientWidth / 2) + 10, elemWidth.clientWidth / 2) - 10);

            casesData [caseindex] = {
                fixСarousel: false,
                //carousel: (elemWidth.clientWidth * (caseItem.Index - 1) + randomCarousel),
                winBlock: caseItem,
                randomBlocks: newItems,
                startRandomBlocks: newItems.slice(caseItem.Index - 3, caseItem.Index + 6),
                carouselStart: randomCarousel
            }
            setTimeout(() => {
                const first = document.querySelector(`#popuponate__roulette .newdonate__roulette-main:nth-child(${caseindex + 1}) .newdonate__roulette-element:nth-child(4)`);
                const realCarousel = document.querySelector(`#popuponate__roulette .newdonate__roulette-main:nth-child(${caseindex + 1}) .newdonate__roulette-element:nth-child(${caseItem.Index + 1})`);
                
                casesData [caseindex].carousel = (realCarousel.getBoundingClientRect().x - first.getBoundingClientRect().x + randomCarousel);

                if (!toggledFast) {
                    let stopToCord = -1;
                    casesData [caseindex].IntervalId = setInterval(() => {
                        if (stopToCord === elemWidth.getBoundingClientRect().left) {
                            clearInterval (casesData [caseindex].IntervalId);
                            casesData [caseindex].IntervalId = null;
                            casesData [caseindex].fixСarousel = true;
                            openPopup ();
                        } else
                            stopToCord = elemWidth.getBoundingClientRect().left;
                    }, 500);
                } else {
                    casesData [caseindex].fixСarousel = true;
                    openPopup ();
                }
            }, 0)
        });
        return;
    }

    const openPopup = () => {
        let toggled = false;
        
        casesData.forEach((caseItem) => {
            if (!caseItem.fixСarousel && caseItem.winBlock && caseItem.winBlock.Item) {
                toggled = true;
            }
        })
        
        if (!toggled)
            SetPopup (true, casesData);
    }
    
    window.events.addEvent("cef.roullete.confirm", Confirm);

    onDestroy(() => {
        for(let i = 0; i < maxCount; i++) {
            if (casesData [i].IntervalId !== null) {
                clearInterval (casesData [i].IntervalId);
                casesData [i].IntervalId = null;
            }
        }
        window.events.removeEvent("cef.roullete.confirm", Confirm);
        executeClient ("client.roullete.confirm", false, -1);      
    });

    const onOpen = (_toggledFast = false) => {
        if (isConfirm)
            return;
        else if (antiFlud > new Date().getTime())
            return;
        //else if ($accountRedbucks < getPrice (caseData.price * currentCount, caseData.index, $accountUnique))
        //    return window.notificationAdd(4, 9, `Недостаточно Redbucks!`, 3000);
        antiFlud = new Date().getTime() + 2500;
        toggledFast = _toggledFast;
        executeClient ("client.roullete.open", caseData.index, currentCount);
    }

    const onCurrentCount = (count) => {
        if (isConfirm)
            return;
        else if (antiFlud > new Date().getTime())
            return;
            
        currentCount  = count;
    }

    let isEndPopup = isPopup;
    $: {
        if (isPopup !== isEndPopup) {
            isEndPopup = isPopup;
            if (!isPopup && isConfirm) {                
                for(let i = 0; i < maxCount; i++) {
                    casesData [i].winBlock = {};
                }
                isConfirm = false;
            }
        }
    }

    const getPrice = (price, index, unique) => {
        if (unique && unique.split("_")) {
            let getData = unique.split("_");
            if (getData[0] === "cases" && Number (getData[1]) === index && Number (getData[2]) === 0) {
                price = Math.round (price * 0.7);
            }
        }
        return price;
    }
</script>

{#if isLoad}

<div class="opencasedon">
    <div class="caseclosed">
        <p>Выйти из меню</p>
        <div class="btncl">
            <p>ESC</p>
        </div>
    </div>
    {#if isPopup}
        <PoputWin {SetPopup} popupData={dataPopup} /> 
    {/if}
    <div class="caseopen">
        <h1>{caseData.name}</h1>
        <div class="caserun">
            <div class="runtop"></div>
            <div id="popuponate__roulette">
                <div class="newdonate__roulette-container">
                    {#each casesData as caseData, indexCase}
                    {#if currentCount > indexCase}
                    <div class="newdonate__roulette-main">
                        <div class="newdonate__roulette-elements" style={`transition: ${caseData.fixСarousel ? "none" : "all 10000ms cubic-bezier(0.32, 0.64, 0.45, 1) 0s"};transform: translate3d(${caseData.fixСarousel ? (0 - caseData.carouselStart) : (0 - caseData.carousel)}px, 0px, 0px)`}>
                            {#each (!caseData.fixСarousel ? caseData.randomBlocks : caseData.startRandomBlocks) as item, index}
                            <div class="newdonate__roulette-element margin-22">
                                <div class="roulette-element__img" style="background-image: url({document.cloud + `img/roulette/${item.image}.png`})" />
                            </div>
                            {/each}
                        </div>
                    </div>
                    {/if}
                    {/each}
                </div>
            </div>
            <div class="runbot"></div>
            <div class="casebtn">
                <div class="caseru" on:keypress={() => {}} on:click={() => onOpen()}>
                    <p>Открыть</p>
                </div>
                <div class="casefast" on:keypress={() => {}} on:click={() => onOpen(true)}>
                    <p>Быстро открыть</p>
                </div>
            </div>
        </div>
        <div class="caseitemlist">
            {#each caseData.items as value, index}
                <div class="items {value.color}">
                    <p>{value.title}</p>
                    <img class="icoi" src="{document.cloud + `img/roulette/${value.image}.png`}" alt=""/>
                </div>
            {/each}
        </div>
    </div>
</div>
{/if}