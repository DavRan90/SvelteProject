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

    let events = $state([]);
    let newEvent = $state({title: "Default title", description: "Default Description", date: "1970-05-01"});
        
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


<div class="grid">
  <h2>Event details:</h2>
  <form onsubmit={addEvent}>
    <div>
      <label>
        Title
        <input type="text" bind:value={newEvent.title}/>
      </label>
    </div>
    <div>
      <label>
        Description
        <input type="text" bind:value={newEvent.description}/>
      </label>
    </div>
    <div>
      <label>
        Date
        <input type="text" bind:value={newEvent.date}/>
      </label>
    </div>
    <button type="submit">Create</button>
  </form>
</div>

<h2>Events</h2>
<div>
    <EventList {events} onDelete={onDelete} onEdit={onUpdate}/>
</div>

<style>
  body {
    font-family: system-ui, sans-serif;
    background: #f6f7f9;
    color: #222;
  }

  h1, h2 {
    margin-bottom: 0.5rem;
  }

  form {
    background: white;
    padding: 1rem;
    border-radius: 8px;
    max-width: 420px;
    box-shadow: 0 6px 16px rgba(0,0,0,0.08);
    margin-bottom: 2rem;
  }

  form > div {
    margin-bottom: 0.75rem;
  }

  label {
    display: flex;
    flex-direction: column;
    font-size: 0.9rem;
    gap: 0.25rem;
  }

  input {
    padding: 0.5rem 0.6rem;
    border-radius: 6px;
    border: 1px solid #ccc;
    font-size: 1rem;
  }

  input:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 2px rgba(79,70,229,0.15);
  }

  button {
    margin-top: 0.5rem;
    padding: 0.6rem 1rem;
    border-radius: 6px;
    border: none;
    background: #4f46e5;
    color: white;
    font-weight: 600;
    cursor: pointer;
  }

  button:hover {
    background: #4338ca;
  }

  ul {
    list-style: none;
    padding: 0;
    max-width: 600px;
  }

  li {
    background: white;
    padding: 0.75rem 1rem;
    border-radius: 8px;
    margin-bottom: 0.5rem;
    box-shadow: 0 2px 8px rgba(0,0,0,0.06);
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
</style>