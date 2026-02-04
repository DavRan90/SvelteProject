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
    export let onEdit: (id: number, updateTitle: string) => void;

    function handleDelete(){
        onDelete(event.id);
    }

    function handleEdit(){
        event.editing = !event.editing;
    }

    function saveEdit(){
        event.editing = !event.editing;
        onEdit(event.id, event.title);
        
    }

</script>


<div>
    <div class="grid">
        {#if !event.editing}
            <p>{event.title}</p>
            <button on:click={handleEdit}>✏️</button>
        {:else}
            <input type="text" bind:value={event.title}/>
            <button on:click={saveEdit}>✅</button>
        {/if}
        
        <button on:click={handleDelete}>🗑️</button>
    </div>
</div>



<style>

    .grid{
        display: grid;
		grid-template-columns: 10em 3em 3em;
		grid-gap: 1em;
		height: 100%;
    }
    textarea {
		flex: 1;
		resize: none;
	}
    input{
        border-radius: 10px;
        padding: 1em;
    }
    label{
        margin: 1em;
    }
    button{
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
    }
    .box{
        font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
        border-style: solid;
        border-width: 1px;
        border-radius: 10px;
        padding: 1em;
        max-width: fit-content;
    }

</style>