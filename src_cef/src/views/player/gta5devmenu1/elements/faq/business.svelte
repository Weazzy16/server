<script>
    import Shop from './elements/businesses/shop.svelte';
    import Barbershop from './elements/businesses/barbershop.svelte';
    import Restaurants from './elements/businesses/restaurants.svelte';
    import CarWash from './elements/businesses/carwash.svelte';
    import ClothingStore from './elements/businesses/clothingstore.svelte';
    import GunShop from './elements/businesses/gunshop.svelte';
    import Lsc from './elements/businesses/lsc.svelte';
    import MaskShop from './elements/businesses/maskshop.svelte';
    import GasStation from './elements/businesses/gasstation.svelte';
    // import AutoMoto from './elements/businesses/automoto.svelte';
    import PetShop from './elements/businesses/petshop.svelte';
    import TattooShop from './elements/businesses/tattooshop.svelte';

    const Views = {
        Shop,
        Barbershop,
        Restaurants, 
        CarWash,
        ClothingStore,
        GunShop,
        Lsc,
        MaskShop,
        GasStation,
        PetShop,
        TattooShop
    }

    let onSelectedView2 = "Page1";

</script>

<div class="menurules">
    <div class="rulesleft">
        <div class="headrules">
            <h1>ВЫБОР КАТЕГОРИИ</h1>
        </div>
        <div class="ruleslist">
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Shop"} class:active={onSelectedView2 == "Shop"}>
                <p>Магазин 24/7</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Barbershop"} class:active={onSelectedView2 == "Barbershop"}>
                <p>Барбершоп</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Restaurants"} class:active={onSelectedView2 == "Restaurants"}>
                <p>Ресторан</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "CarWash"} class:active={onSelectedView2 == "CarWash"}>
                <p>Мойка</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "ClothingStore"} class:active={onSelectedView2 == "ClothingStore"}>
                <p>Магазин одежды</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "GunShop"} class:active={onSelectedView2 == "GunShop"}>
                <p>Оружейный магазин</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Lsc"} class:active={onSelectedView2 == "Lsc"}>
                <p>ЛСК</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "MaskShop"} class:active={onSelectedView2 == "MaskShop"}>
                <p>Магазин масок</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "GasStation"} class:active={onSelectedView2 == "GasStation"}>
                <p>АЗС</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "PetShop"} class:active={onSelectedView2 == "PetShop"}>
                <p>Магазин питомцев</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "TattooShop"} class:active={onSelectedView2 == "TattooShop"}>
                <p>Тату-салон</p>
            </div>
        </div>
    </div>
    <div class="rulesright">
        <svelte:component this={Views[onSelectedView2]}/>
    </div>
</div>