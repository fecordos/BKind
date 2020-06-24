# :rainbow: BKind


:high_brightness: Functionalitati implementate:

- Aplicatia are doua tipuri de utilizatori:

   - cei care au rol de admin ("Administrators") - operatii CRUD
   - utilizatori obisnuiti, cu drepturi restranse
   - Nota: adminii pot crea si alte roluri la care sa adauge ulterior anumiti utilizatori ai aplicatiei (role management), 
dar gestioneaza si utilizatorii aplicatiei (user management).

- Inregistrarea utilizatorului:

   - in momentul creării contului, acesta primeste un e-mail cu link-ul unde poate confirmara adresa de mail (SendGrid)
   - daca adresa nu este confirmata, user-ul nu se poate loga in aplicatie (email confirmation required)
 
- Autentificarea utilizatorului:

  - dupa confirmare, utilizatorul se poate loga
  - daca user-ul nu este la prima logare si nu isi aminteste parola, poate solicita "Ai uitat parola?"; 
  - va trebui sa specifice o adresa de mail la care sa primeasca link-ul unde sa-si modifice parola (SendGrid)
  - autentificare cu succes => poate vizualiza postarile existente (pagina "Acasa")
  - poate realiza cautari printre postari (search bar)
  - pagina principala "Acasa" prezinta o sectiune in partea stanga, care pentru un user obisnuit are urmatoarele : 
  
     a. "Istoricul meu" - unde se vor salva postarile salvate de user/cererile pe care vrea sa le indeplineasca ;
     
     b. Setari profil (vizualizare/editare date cont, schimbare parola curenta, descarcare date personale, stergere definitiva a contului);
     
     c. Filtrare cereri dupa categorii ;
     
     d. Pentru admini prezinta, in plus: Lista adreselor, Lista persoanelor, Lista categoriilor, Adaugare cereri - pentru gestionarea/realizarea de operatii CRUD asupra bazei de date a aplicatiei;
  - pagina "Acasa" contine postarile adaugate de admin : 
            - user-ul obisnuit: vizualizare detalii + salvare postare in "Istoricul meu" ;
            - pentru admini, are in plus: Edit, Delete postare/request, sectiunea unde pot gasi "Gestionare conturi" si "Gestionare roluri");

 - Salvarea/eliminarea postarii, ce prezinta interes pentru user, la pagina "Istoricul meu".
 - Creare cookie persistent si sesiune temporara (selectare "Remember me")
 - Filtrarea postarilor dupa categoriile existente
 - Functionalitatea de mesagerie (creare ChatRoom/sectiune comuna tuturor utilizatorilor, folosind SignalR pentru servicii real-time, unde utilizatorii pot interactiona si cu administratorii aplicatiei, unde pot pune intrebari/posta anunturi)


