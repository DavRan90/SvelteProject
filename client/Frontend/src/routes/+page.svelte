<script lang="ts">
    import EventList from "$lib/EventList.svelte";
    import { onMount } from 'svelte';
    import { getEvents, createEvent } from '$lib/api';

    let events = $state([
        { id: 1, title: "Titel", description: "Desc", date: "1970-01-05", editing: false},
        { id: 2, title: "Titel2", description: "Beskr", date: "1970-01-08", editing: false},])

        let newEvent = $state({title: "Titel", description: "Description", date: "1970-05-01"});
        
    onMount(async () => {
    events = await getEvents();
  });

        function addEvent()
        {
            if (newEvent.title.trim() === "") return;

            const newId = events.length ? Math.max(...events.map(e => e.id)) + 1 : 1;

            events = [
                ...events,
                { id: newId, title: newEvent.title, description: newEvent.description, date: newEvent.date, editing: false }
            ];
            newEvent = {title: "", description: "", date: ""}
        }

        function deleteEvent(id: number){
            let deletedEvent = events.filter(e => e.id == id)
            alert("Deleted " + deletedEvent[0].title)
            events = events.filter(e => e.id !== id)
        }

        function editEvent(id: number, newText: string){
            events = events.map(event =>
                event.id === id
                    ? { ...event, text: newText}
                    : event
        );
        }
</script>

<div class="box">
    <h2>Event details:</h2>
    <div class="grid">
        <p>Title of event</p>
        <input type="text" bind:value={newEvent.title}/>
        <p>Description of event</p>
        <input type="text" bind:value={newEvent.description}/>
        <p>Date of event</p>
        <input type="text" bind:value={newEvent.date}/>
        <button onclick={addEvent}>Add event</button>
    </div>
</div>

<h2>Events</h2>
<div>
    <EventList {events} onDelete={deleteEvent} onEdit={editEvent}/>
</div>



<style>
    ul {
        list-style: none;
        padding: 0;
        width: 250px;
        min-height: 250px;
    }

    li {
        display: flex;
        gap: 0.5rem;
        align-items: center;
        padding: 0.4rem;
        border-radius: 6px;
        background: #f5f5f5;
        margin-bottom: 0.4rem;
    }
    .grid{
        display: grid;
		grid-template-columns: 10em 20em;
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
    button:hover
    {
        background-color: rgb(240, 245, 255);
        scale: 115%;
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