
<script>
    import { format } from "api/formatter";
    import { executeClientToGroup , executeClientAsyncToGroup } from "api/rage";
    export let closeMenu;

    let countLoad = 10;

    let listData = [];
    const onLoad = (count) => {
        countLoad = count;
        executeClientAsyncToGroup("truck.getList", count).then((result) => {
            if (result && typeof result === "string")
                listData = JSON.parse(result);
        });
    }

    onLoad (10);

    const onTakeOrder = (uid) => {
        if (!window.loaderData.delay ("truck.onTakeOrder", 2))
            return;

        executeClientToGroup ("truck.take", uid);
    }
    import { fade } from 'svelte/transition'

</script>

<div in:fade class="box-100">
    <div class="box-between newphone__project_padding20">
        <div class="newphone__maps_header">Aufträge</div>
        <div class="phoneicons-add1" on:keypress={() => {}} on:click={closeMenu}></div>
    </div>
    
    <div class="newphone__maps_list newphone__maps_clients">
        {#if listData && typeof listData === "object" && listData.length > 0}
            {#each listData as order}
                <div class="newphone__maps_client">
                    <div class="box-between">
                        <div class="box-column">
                            <div class="greenid">№{order.uid}</div>
                            <div class="box-flex">
                                <div class="newmphone__client_circle mech"></div>
                                <div>{order.area}</div>
                            </div>
                            <div class="gray">Entfernung {order.dist} m.</div>
                            <div>Ware: <span class="violet">{order.name}</span></div>
                            <div>Sie verdienen: <span class="green">${format("money", order.price)}</span></div>
                        </div>
                        <div class="phoneicons-Button newphone__chevron-back"></div>
                    </div>
                    <div class="newphone__client_button" on:keypress={() => {}} on:click={() => onTakeOrder (order.uid)}>Auftrag annehmen</div>
                </div>
            {/each}
            <div class="newphone__project_button" on:keypress={() => {}} on:click={() => onLoad (10 + countLoad)}>Aufräge laden</div>
        {:else}
            <div class="newphone__forbes_anon newphone__project_padding20">
                <div class="newphone__forbes_hidden noneelements"></div>
                <div class="gray">Zurzeit gibt es keine Aufträge</div>
            </div>
        {/if}
    </div>
</div>