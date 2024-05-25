import axios from "axios"

export const fetchNotes = async () => {
    var response = await axios.get("https://localhost:7263/api/notes")
    var notes = await response.data.notes

    return notes
}
export const createNote = async (note) => {
    var response = await axios.post("https://localhost:7263/api/notes", note)

}
export const deleteNote = async (id) => {
    var response = await axios.delete(`https://localhost:7263/api/notes?id=${id}`)

}
export const updateNote = async (note) => {
    var response = await axios.put("https://localhost:7263/api/notes", note)

}