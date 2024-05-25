import {
  Divider,
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Text,
  StepSeparator,
  IconButton,
  Heading,
  Flex,
  ButtonGroup,
  Spacer,
  Input,
  useBoolean,
  useStatStyles,
  Textarea,
} from "@chakra-ui/react";
import { DeleteIcon, EditIcon, CheckIcon, CloseIcon } from "@chakra-ui/icons";
import { useEffect, useState } from "react";
import moment from "moment/moment";

let saveNote = null;
const MyCard = ({ id, title, description, time, deleteNote, updateNote }) => {
  const [isEdited, setEdited] = useBoolean(false);
  const [note, setNote] = useState({
    id: id,
    title: title,
    description: description,
  });

  const toggleEdit = () => {
    setEdited.toggle();
    if (isEdited == true) {
      updateNote(note);
    } else {
      saveNote = { ...note };
    }
  };
  const cancelEdit = () => {
    setEdited.toggle();
    setNote({ ...saveNote });
  };
  return (
    <Card variant="filled">
      <CardHeader>
        <Flex alignItems="center">
          {isEdited ? (
            <Input
              fontSize="4xl"
              fontWeight="bold"
              value={note.title}
              onChange={(e) => setNote({ ...note, title: e.target.value })}
            ></Input>
          ) : (
            <Heading>{note.title}</Heading>
          )}
          <Spacer />
          <ButtonGroup>
            {isEdited ? (
              <>
                <IconButton
                  onClick={cancelEdit}
                  aria-label="Cancel edit note"
                  icon={<CloseIcon />}
                />
                <IconButton
                  onClick={toggleEdit}
                  aria-label="Confirm edit note"
                  icon={<CheckIcon />}
                />
              </>
            ) : (
              <IconButton
                onClick={toggleEdit}
                aria-label="Edit note"
                icon={<EditIcon />}
              />
            )}

            <IconButton
              onClick={() => deleteNote(id)}
              aria-label="Delete note"
              icon={<DeleteIcon />}
            />
          </ButtonGroup>
        </Flex>
      </CardHeader>
      <Divider />
      <CardBody>
        {isEdited ? (
          <Textarea
            onChange={(e) => setNote({ ...note, description: e.target.value })}
            value={note.description}
            wordBreak="break-word"
          ></Textarea>
        ) : (
          <Text wordBreak="break-word">{note.description}</Text>
        )}
      </CardBody>
      <Divider />

      <CardFooter flex justifyContent="flex-end">
        {moment(time).format("YYYY/MM/DD HH:mm:ss")}
      </CardFooter>
    </Card>
  );
};

export default MyCard;
