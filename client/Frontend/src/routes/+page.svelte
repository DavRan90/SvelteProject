<script lang="ts">
    import EventList from "$lib/EventList.svelte";
    import { onMount } from 'svelte';
    import { getEvents, createEvent, updateEvent, deleteEvent } from '$lib/api';

    interface Event {
        id: number;
        title: string;
        description: string;
        date: string;
        editing: boolean;
    }

    let events = $state([
        { id: 1, title: "Titel", description: "Desc", date: "1970-01-05", editing: false},
        { id: 2, title: "Titel2", description: "Beskr", date: "1970-01-08", editing: false},])

        let newEvent = $state({title: "Titel", description: "Description", date: "1970-05-01"});
        
    onMount(async () => {
    events = await getEvents();
  });
        async function addEvent() {
            await createEvent({
                title: newEvent.title,
                description: newEvent.description,
                date: newEvent.date
                });
            events = await getEvents();
            newEvent = {title: "", description: "", date: ""}
        }

        async function onDelete(id: number){
            await deleteEvent(id);
            events = await getEvents();
        }

        async function onUpdate(id: number, event: Event) {
            await updateEvent(id, {
            title: event.title,
            description: event.description,
            date: event.date
            });
            events = await getEvents();
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

<form onsubmit={submit}>
  <div>
    <label>
      Title
      <input
        type="text"
        bind:value={newEvent.title}
        required
      />
    </label>
  </div>

  <div>
    <label>
      Description
      <input
        type="text"
        bind:value={newEvent.description}
      />
    </label>
  </div>

  <div>
    <label>
      Date
      <input
        type="date"
        bind:value={newEvent.date}
        required
      />
    </label>
  </div>

  <button type="submit">Create</button>
</form>

<h2>Events</h2>
<div>
    <EventList {events} onDelete={onDelete} onEdit={onUpdate}/>
</div>



<style>
    .grid{
        display: grid;
		grid-template-columns: 10em 20em;
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