<script>
    import { format } from "api/formatter";
    import { executeClientToGroup , executeClientAsyncToGroup } from "api/rage";
    export let closeMenu;

    let listData = [];
    const updateData = () => {
        executeClientAsyncToGroup("taxijob.getList").then((result) => {
            if (result && typeof result === "string")
                listData = JSON.parse(result);
        });
    }
    updateData ();
    import { addListernEvent } from 'api/functions';
    addListernEvent ("phone.taxijob.update", updateData);

    const onTakeOrder = (id) => {
        if (!window.loaderData.delay ("taxijob.onTakeOrder", 2))
            return;

        executeClientToGroup ("taxijob.take", id);
    }
    import { fade } from 'svelte/transition'
</script>

<div in:fade class="box-100">
    <div class="box-between newphone__project_padding20">
        <div class="newphone__maps_header">Aufträge</div>
        <div class="phoneicons-add1"on:keypress={closeMenu} on:click={closeMenu}></div>
        <!----<div class="phoneicons-add1" on:keypress={() => {}} on:click={closeMenu}></div>-->
    </div>
    
    <div class="newphone__maps_list newphone__maps_clients">
        {#if listData && typeof listData === "object" && listData.length > 0}
            {#each listData as order}
                <div class="newphone__maps_client">
                    <div class="box-between">
                        <div class="box-column">
                            <div class="box-flex">
                                <div class="newmphone__client_circle to"></div>
                                <div>{order.area}</div>
                            </div>
                            <div class="gray">Entfernung {order.dist} m.</div>
                            <div>Kunde: <span class="orange">{order.name}</span></div>
                            <!--<div>Вы получите: <span class="green">${format("money", order.price)}</span></div>-->
                        </div>
                        <div class="phoneicons-Button newphone__chevron-back"></div>
                    </div>
                    <div class="newphone__client_button" on:click={() => take (-1)}>Bestellung aufgeben</div>
                </div>
            {/each}
        {:else}
        <div class="newphone__maps_client">
            <div class="box-between">
                <div class="box-column">
                    <div>Kunde: <span class="orange">Wichtiger Kunde</span></div>
                </div>
                <div class="phoneicons-Button newphone__chevron-back"></div>
            </div>
            <div class="newphone__client_button" on:click={() => onTakeOrder (-1)}>Auftrag annehmen</div>
        </div>
        {/if}
    </div>
</div>