<script>
    import HomeButton from './../../homebutton.svelte'

    export let setPosition;
    export let setOtherElement;
    export let closeMenu;


    import { executeClientAsync, executeClientToGroup, executeClientAsyncToGroup } from 'api/rage'


    let streetName = "";
    let areaName = "";
    const getStreetAndArea = (pos) => {

        executeClientAsync("getStreetName", pos).then((result) => {
            streetName = result;
        })

        executeClientAsync("getAreaName", pos).then((result) => {
            areaName = result;
        });
    }



    //

    const onOrder = () => {
        if (!window.loaderData.delay ("taxi.onOrder", 1.5))
            return;

        executeClientToGroup ("taxi.order");
    }

    const onCancelOrder = () => {
        if (!window.loaderData.delay ("taxi.onOrder", 1.5))
            return;

        executeClientToGroup ("taxi.cancel");
    }


    let selectTaxi = {}

    export let position;

    const getData = () => {
        executeClientAsyncToGroup("taxi.getOrder").then((result) => {
            if (result && typeof result === "string") {
                selectTaxi = JSON.parse(result);

                if (selectTaxi.pos) {
                    setPosition (selectTaxi.pos);

                    getStreetAndArea(selectTaxi.pos);
                } else
                    getStreetAndArea(position);

                setOtherElement (otherElement);
            }
        });
    }

    getData();
    import { addListernEvent } from 'api/functions'
    addListernEvent ("phone.taxi.load", getData);
    import { fade } from 'svelte/transition'

    let otherElement;
</script>

<div class="newphone__maps_categories" in:fade>
    {#if selectTaxi && selectTaxi.driver}
        <div class="newphone__maps_price">
            <div class="box-column">
                <div class="newphone__maps_pricetitle">Fahrer:</div>
                <div class="newphone__maps_pricesubtitle">{selectTaxi.driver}</div>
                <div class="newphone__maps_pricetitle">{selectTaxi.number}</div>
            </div>
            <div class="newphone__maps_car"></div>
        </div>
    {/if}

    <div class="newphone__maps_subcategories" bind:this={otherElement} use:setOtherElement>

        <div class="box-between newphone__project_padding20">
            <div class="newphone__maps_header">Taxi rufen</div>
            <div class="phoneicons-add1" on:keypress={() => {}} on:click={closeMenu}></div>
        </div>

        <div class="box-flex newphone__project_padding20 mb-0">
            <div class="newmphone__maps_circle"><div class="newmphone__maps_circle2"></div></div>
            <div class="newphone__maps_column">
                <div class="newphone__column_title">Abholort</div>
                <div class="newphone__column_subtitle">{streetName} - {areaName}</div>
            </div>
        </div>
        <div class="newphone__maps_list" style="height: auto; min-height: auto; max-height: auto;">
            {#if selectTaxi && selectTaxi.isOrder}
                <div class="newphone__maps_title">Bestellung aufgegeben</div>
                <div class="newphone__maps_subtitle">Warten Sie auf den Fahrer, ohne den Ort zu verlassen. Wenn Sie Ihre Stelle
                    verlassen, dann wird die Bestellung storniert und Ihnen wird eine Geldstrafe auferlegt.
                </div>
                <!--<div class="orange">Стоимость: 2500$</div>-->
                <div class="newphone__project_button" on:keypress={() => {}} on:click={onCancelOrder}>Abbrechen</div>
            {:else}
                <div class="newphone__project_button" on:keypress={() => {}} on:click={onOrder}>Bestellen</div>
            {/if}
            <div class="orange box-center" on:keypress={() => {}} on:click={closeMenu}>Schließen</div>
        </div>
        <HomeButton />
    </div>
</div>