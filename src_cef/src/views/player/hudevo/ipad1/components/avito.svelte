<script>
    import Header from './header.svelte'
    import HomeButton from './homebutton.svelte'

    let categorieName = {
        cars : "Fahrzeug",
        houses : "Immobilien",
        business : "Business", 
        clothes : "Kleidung",
        other : "Andere",
    }

    let categoriesFilter = {
        cars : false,
        houses : true,
        business : true, 
        clothes : true,
        other : true,
    }

    let categoriesList = [
        "cars",
        "houses",
        "business",
        "clothes",
        "other",
    ]

    let newsList = [
        {
            name: "Robert",
            text: "Verkaufe Bugatti Chiron 10.000.000$",
            url: "https://sun9-14.userapi.com/impg/UtgEdz-aGEqiiissDBfPvjBrCXgni84GR7fBYg/DB_tRyCZ4SI.jpg?size=700x500&quality=96&sign=dd1141adfae475aa4c23f6499b4d0d58&type=album",
            price: "50000",
            categorie: "cars"
        },
        {
            name: "Robert",
            text: "Verkaufe Infernus 10.000.00$",
            price: "50000",
            url: "",
            categorie: "cars"
        },
        {
            name: "Robert",
            text: "Verkaufe nichts",
            price: null,
            url: "https://sun7-6.userapi.com/impg/1SSbALSIKg8VEvpIpqYKYHICXfkOCQ-dBNEzGw/1yMunuNp4Pw.jpg?size=1080x1080&quality=96&sign=ed2017528daac94a8fc4238bc5daaf1d&type=album",
            categorie: "other"
        },
    ]
    
    let currentElement = "list"
    let addSelector = null;
    
    function goToStartPage(){
        currentElement = "list"
        addSelector = null;
    }


    import { onInputFocus, onInputBlur } from "@/views/player/hudevo/phonenew/data";

    import { onDestroy } from 'svelte'
    onDestroy(() => {
        onInputBlur ();
    });
</script>
<div class="newphone__rent">
    <Header />
    <div class="newphone__rent_content">
        <div class="box-flex newphone__project_padding20 p-top">
            <div class="newphone__maps_headerimage ravito"></div>
            <div class="newphone__maps_headertitleGPS">R-Avito</div>
        </div>
        {#if currentElement == "list"}
            <div class="newphone__project_button ravito" on:keypress={() => {}} on:click={()=> currentElement = "create"}>Anzeige aufgeben</div>
            <div class="box-center box-between newphone__project_padding20">
                <div class="newphone__maps_header m-0">Anzeige:</div>
                {#if false}
                    <div class="newphone__button_filter" on:keypress={() => {}} on:click={()=> currentElement = "filter"}>
                        <span class="phoneicons-filter"></span>
                        Filter
                    </div>
                {/if}
                {#if true}
                <div class="newphone__button_filter box-center violet__background" on:keypress={() => {}} on:click={()=> currentElement = "filter"}>
                    2 Filter
                    <span class="phoneicons-close"></span>
                </div>
            {/if}
            </div>
            <div class="newphone__ads_list">
                {#each newsList as item}
                    <div class="newphone__ads_element">
                        <div class="box-between">
                            <div class="newphone__ads_categorie">#{categorieName[item.categorie]}</div>
                            <div class="newphone__ads_gray">13:48 10.09.2021</div>
                        </div>
                        <div class="newphone__ads_text">{item.text}</div>
                        {#if item.price}
                        <div class="newphone__ads_violet">${item.price}</div>
                        {/if}
                        {#if item.url}
                            <div class="newphone__ads_image" style="background-image: url('{item.url}')"></div>
                        {/if}
                        <div class="box-between">
                            <div class="gray">{item.name}</div>
                            <div class="box-flex">
                                <div class="newphone__ads_circle violet__background">
                                    <span class="phoneicons-call"></span>
                                </div>
                                <div class="newphone__ads_circle violet__background">
                                    <span class="phoneicons-chat"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                {/each}
            </div>
        {/if}
        {#if currentElement == "create" && addSelector === null}
            <div class="newphone__maps_header m-top newphone__project_padding20">Anzeige erstellen:</div>
            <div class="newphone__ads_list">
                {#each categoriesList as item}
                    <div class="newphone__project_categorie" on:keypress={() => {}} on:click={()=> addSelector = item}>
                        <div>{categorieName[item]}</div>
                        <div class="phoneicons-Button newphone__chevron-back violet"></div>
                    </div>
                {/each}
            </div>
        {/if}
        {#if currentElement == "filter" && addSelector === null}
            <div class="newphone__maps_header m-top newphone__project_padding20">Filter:</div>

            <div class="newphone__ads_list filter">
                {#each categoriesList as item, index}
                    <div class="box-between w-100">
                        <div>{categorieName[item]}</div>
                        <div class="newphone__checkbox">
                            <input class="styled-checkbox" id="styled-checkbox-1" type="checkbox" value="value{index}" bind:checked={categoriesFilter[item]}>
                            <label class="styled-checkbox1" for="styled-checkbox-1"></label>
                        </div>
                    </div>
                {/each}
            </div>
            <div class="box-between w-1 newphone__project_padding20">
                <div class="box-column">
                    <div class="newphone__ads_title newphone__project_padding20">Preis von:</div>
                    <input type="text" class="newphone__ads_input small" placeholder="Text eingeben" on:focus={onInputFocus} on:blur={onInputBlur}>
                </div>
                <div class="box-column">
                    <div class="newphone__ads_title newphone__project_padding20">Preis bis:</div>
                    <input type="text" class="newphone__ads_input small" placeholder="Text eingeben" on:focus={onInputFocus} on:blur={onInputBlur}>
                </div>
            </div>
            <div class="newphone__project_button ravito">Filter anwenden</div>
            <div class="violet box-center" on:keypress={() => {}} on:click={goToStartPage}>zurück</div>
        {/if}
        {#if addSelector}
            <div class="newphone__maps_header m-top newphone__project_padding20">{categorieName[addSelector]}</div>
            <div class="newphone__ads_title newphone__project_padding20">Titel:</div>
            <input type="text" class="newphone__ads_input" placeholder="Text eingeben" on:focus={onInputFocus} on:blur={onInputBlur}>
            <div class="newphone__ads_info small">
                <textarea class="newphone__ads_textarea" placeholder="Text eingeben"></textarea>
                <div class="newphone__ads_capture">
                    {#if false}
                        <div class="newphone__ads_gray">Foto hinzufügen+</div>
                    {/if}
                    {#if true}
                        <div class="newphone__ads_addimg" style="background-image: url('https://sun9-4.userapi.com/impg/hGdlzARcuXX56OtsDm-6Wpc1U8uT6XgsMOoU1g/CDInqyh-Md4.jpg?size=784x449&quality=96&sign=53215729fafd6ec37e6d10a7fc184864&type=album')"></div>
                        <div class="box-column">
                            <span class="phoneicons-reload violet"></span>
                            <span class="phoneicons-trash violet"></span>
                        </div>
                    {/if}
                </div>
            </div>
            <div class="newphone__ads_title newphone__project_padding20">Preis:</div>
            <input type="text" class="newphone__ads_input" placeholder="Text eingeben" on:focus={onInputFocus} on:blur={onInputBlur}>
            <div class="newphone__project_button ravito">Anzeige aufgeben</div>
            <div class="violet box-center" on:keypress={() => {}} on:click={goToStartPage}>Zurück</div>
        {/if}
    </div>
    <HomeButton />
</div>