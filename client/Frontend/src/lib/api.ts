const API_BASE = 'http://localhost:5242';

export async function getEvents() {
  const res = await fetch(`${API_BASE}/events`);
  if (!res.ok) throw new Error('Failed to load events');
  return res.json();
}

export async function createEvent(event: {
  title: string;
  date: string;
}) {
  const res = await fetch(`${API_BASE}/events`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(event)
  });

  if (!res.ok) throw new Error('Failed to create event');
  return res.json();
}