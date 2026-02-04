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
            <button onclick={handleEdit}>✏️</button>
        {:else}
        <div>
            <form onsubmit={saveEdit}>
                <div>
                <label>
                    Title
                    <input type="text" bind:value={event.title}/>
                </label>
                </div>
                <div>
                <label>
                    Description
                    <input type="text" bind:value={event.description}/>
                </label>
                </div>
                <div>
                <label>
                    Date
                    <input type="text" bind:value={event.date}/>
                </label>
                </div>
                <div>
                    <button type="submit">Save</button>
                </div>
            </form>
            <!-- <input type="text" bind:value={event.title}/>
            <input type="text" bind:value={event.description}/>
            <input type="text" bind:value={event.date}/> -->
        </div>
            <button class="disabled emoji">✏️</button>
        {/if}
        <button onclick={handleDelete} class="emoji">🗑️</button>
    </div>
</div>



<style>
    p {
        margin-left: 1em;
    }
    button {
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
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
    .emoji{
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
        max-width: 4em;
        max-height: 4em;
    }
    .disabled{
        background-color: grey;
    }

</style>