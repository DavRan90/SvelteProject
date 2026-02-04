<script lang="ts">

    interface Event {
        id: number;
        title: string;
        description: string;
        date: string;
        editing: boolean;
    }

    export let event: Event;
    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, event: Event) => void;

    function handleDelete(){
        onDelete(event.id);
    }

    function handleEdit(){
        event.editing = !event.editing;
    }

    function saveEdit(){
        event.editing = !event.editing;
        onEdit(event.id, event);
    }

</script>


<div>
    <div class="grid">
        {#if !event.editing}
            <p>{event.title}</p>
            <button on:click={handleEdit}>✏️</button>
        {:else}
        <div>
            <input type="text" bind:value={event.title}/>
            <input type="text" bind:value={event.description}/>
            <input type="text" bind:value={event.date}/>
        </div>
            <button on:click={saveEdit}>✅</button>
        {/if}
        <button on:click={handleDelete}>🗑️</button>
    </div>
</div>



<style>
    p {
        margin-left: 1em;
    }
    input{
        margin: 1em;
    }

    .grid{
        display: grid;
		grid-template-columns: 15em 3em 3em;
		grid-gap: 1em;
		height: 100%;
    }
    input{
        border-radius: 10px;
        padding: 1em;
    }
    button{
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
        max-width: 4em;
        max-height: 4em;
    }

</style>