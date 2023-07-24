#Création de notre BDD
DROP DATABASE IF EXISTS fleurs;
CREATE DATABASE IF NOT EXISTS fleurs;
USE fleurs;


#Création des tables
CREATE TABLE IF NOT EXISTS statut(
    type_fidelite VARCHAR(10) PRIMARY KEY,
    reduction INT
);
CREATE TABLE IF NOT EXISTS clients(
	id_client INT AUTO_INCREMENT PRIMARY KEY,
	nom_client VARCHAR(50),
	prenom_client VARCHAR(50),
	num_tel VARCHAR(15), #If you're going to store your phone numbers as integers, you are going to have major limitations and problems later.
	courrier VARCHAR(50), 
	mot_de_passe VARCHAR(50),
	adresse_facturation VARCHAR(100),
	carte_credit VARCHAR(50),
    date_inscription DATE,
	type_fidelite VARCHAR(10), FOREIGN KEY (type_fidelite) REFERENCES statut(type_fidelite)
);

CREATE TABLE IF NOT EXISTS boutique(
	num_boutique INT PRIMARY KEY,
    adresse_boutique VARCHAR(50),
    ville_boutique VARCHAR(25)
);

CREATE TABLE IF NOT EXISTS items(
	id_items INT AUTO_INCREMENT PRIMARY KEY,
    nom_item VARCHAR(25),
    prix_item DECIMAL,
    type_item VARCHAR(25),
    disponibilite BOOLEAN,
    stock INT
);

CREATE TABLE IF NOT EXISTS bouquet_standard(
	num_bouquet INT PRIMARY KEY,
    categorie VARCHAR(50),
    prix INT,
    nom_bouquet VARCHAR(20)
);
CREATE TABLE IF NOT EXISTS commande(
	num_commande VARCHAR(10) PRIMARY KEY,
    date_commande DATE,
    date_livraison DATE,
    adresse_livraison VARCHAR(100),
    message VARCHAR(250),
    code_commande VARCHAR(5),
    prix INT,
    id_client INT, FOREIGN KEY (id_client) REFERENCES clients(id_client),
    num_boutique INT, FOREIGN KEY (num_boutique) REFERENCES boutique(num_boutique)
);

CREATE TABLE IF NOT EXISTS commande_standard(
	id_standard INT AUTO_INCREMENT PRIMARY KEY,
	num_commande VARCHAR(10), FOREIGN KEY (num_commande) REFERENCES commande(num_commande),
    num_bouquet INT, FOREIGN KEY (num_bouquet) REFERENCES bouquet_standard(num_bouquet),
	designer VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS composition(
	num_bouquet INT, FOREIGN KEY (num_bouquet) REFERENCES bouquet_standard(num_bouquet),
    id_items INT, FOREIGN KEY (id_items) REFERENCES items(id_items),
    quantite INT,
    PRIMARY KEY(num_bouquet, id_items)
);

CREATE TABLE IF NOT EXISTS commande_personnalisee(
	id_personnalisee INT AUTO_INCREMENT PRIMARY KEY, 
	num_commande VARCHAR(10), FOREIGN KEY (num_commande) REFERENCES commande(num_commande),
    description_client VARCHAR(250),
    prix_max INT
);

CREATE TABLE IF NOT EXISTS confection(
	id_personnalisee INT, FOREIGN KEY (id_personnalisee) REFERENCES commande_personnalisee(id_personnalisee),
    id_items INT, FOREIGN KEY (id_items) REFERENCES items(id_items),
	quantite INT
);

#Insertion des valeurs dans les tableaux

#-----Statut-----
INSERT INTO statut VALUES ('OR', 15);
INSERT INTO statut VALUES ('BRONZE', 5);

#-----Boutique-----
INSERT INTO boutique VALUES (1, '123 Boulevard Massena', 'Paris');
INSERT INTO boutique VALUES (2, '12 Rue de Lorraine', 'Levallois-Perret');
INSERT INTO boutique VALUES (3, '10 Rue de Longchamp', 'Neuilly-Sur-Seine');
INSERT INTO boutique VALUES (4, '25 Rue de Alma', 'Courbevoie');
INSERT INTO boutique VALUES (5, '68 Rue des Gros Grès', 'Colombes');

#-----Items-----
INSERT INTO items VALUES (1,'Gerbera', 5, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (2,'Ginger', 4, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (3,'Glaïeul', 1, 'Fleur', FALSE, 10); #Disponible de mai à novembre
INSERT INTO items VALUES (4,'Marguerite', 2.25, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (5,'Rose Rouge', 2.50, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (6, 'Rose Blanche', 3, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (7, 'Oiseaux du paradis', 10, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (8, 'Genêt', 4, 'Fleur', TRUE, 10); #Disponible en printemps et été
INSERT INTO items VALUES (9, 'Lys', 7, 'Fleur', TRUE, 10); #mai-juin jusqu'en septembre pour les plus tardifs
INSERT INTO items VALUES (10, 'Alstroméria', 3, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (11, 'Orchidées', 12, 'Fleur', TRUE, 10);
INSERT INTO items VALUES (12, 'Vases', 25, 'Accessoire', TRUE, 10);
INSERT INTO items VALUES (13, 'Paniers', 15, 'Accessoire', TRUE, 5);
INSERT INTO items VALUES (14, 'Rubans', 5, 'Accessoire', TRUE, 20);
INSERT INTO items VALUES (15, 'Papier d''emballage', 10, 'Accessoire', TRUE, 15);
INSERT INTO items VALUES (16, 'Cartes de vœux', 3, 'Accessoire', TRUE, 50);
INSERT INTO items VALUES (17, 'Vaporisateurs', 6, 'Accessoire', TRUE, 25);
INSERT INTO items VALUES (18, 'Bougies et porte-bougies', 20, 'Accessoire', TRUE, 10);
INSERT INTO items VALUES (19, 'Lampe',7, 'Accessoire', FALSE, 2);


update items set stock = stock + 100;


#-----Bouquet standard-----
INSERT INTO bouquet_standard VALUES (1, "Toute occasion", 45, "Gros Merci");
INSERT INTO bouquet_standard VALUES (2, "St-Valentin", 65, "L'amoureux");
INSERT INTO bouquet_standard VALUES (3, "Toute occasion", 40, "L'Exotique");
INSERT INTO bouquet_standard VALUES (4, "Fête des mères", 80, "Maman");
INSERT INTO bouquet_standard VALUES (5, "Mariage", 120, "Vive la mariée");

#-----Composition des bouquets standards-----
INSERT INTO composition VALUES (1,4,9);
INSERT INTO composition VALUES (2,6,8); 
INSERT INTO composition VALUES (2,5,16);
INSERT INTO composition VALUES (3,2,1);
INSERT INTO composition VALUES (3,7,1);
INSERT INTO composition VALUES (3,5,4); 
INSERT INTO composition VALUES (3,6,4);
INSERT INTO composition VALUES (3,8,1); 
INSERT INTO composition VALUES (4,1,4);
INSERT INTO composition VALUES (4,6,4); 
INSERT INTO composition VALUES (4,9,5); 
INSERT INTO composition VALUES (4,10,4); 
INSERT INTO composition VALUES (5,9,5); 
INSERT INTO composition VALUES (5,11,7); 

#-----Clients-----
INSERT INTO clients VALUES (1,'Clef', 'Pierre', '06123456789', 'pierre.clef@gmail.com', 'pierre123', '2 Rue Imaginaire, Paris', '1234 5678 9123 4567',"2023-04-01", NULL);
INSERT INTO clients VALUES (2,'Dupont', 'Alice', '0601020304', 'alice.dupont@mail.com', 'mdp123', '3 Rue des Fleurs, Paris', '1234 5678 9012 3456',"2023-05-02", NULL);
INSERT INTO clients VALUES (3,'Martin', 'Bob', '0605060708', 'bob.martin@mail.com', 'mdp456', '5 Rue des Champs, Paris', '2345 6789 0123 4567',"2023-06-01", NULL);
INSERT INTO clients VALUES (4,'Durand', 'Charles', '0607080910', 'charles.durand@mail.com', 'mdp789', '7 Rue des Arbres, Paris', '3456 7890 1234 5678',"2023-06-10", NULL);
INSERT INTO clients VALUES (5, 'Petit', 'Dominique', '0610111213', 'dominique.petit@mail.com', 'mdpabc', '1 rue de la Paix', '4567 8901 2345 6789',"2023-06-15", NULL);
#Not yet
INSERT INTO clients VALUES (6,'Moreau', 'Emilie', '0614151617', 'emilie.moreau@mail.com', 'mdpdef', '2 rue des Lilas', '5678 9012 3456 7890', "2022-03-15",NULL);
INSERT INTO clients VALUES (7,'Lefebvre', 'Fabien', '0618192021', 'fabien.lefebvre@mail.com', 'mdpghi', '3 avenue de la République', '6789 0123 4567 8901', "2022-05-20",NULL);
INSERT INTO clients VALUES (8,'Rousseau', 'Géraldine', '0622232425', 'geraldine.rousseau@mail.com', 'mdpjkl', '4 rue de la Liberté', '7890 1234 5678 9012', "2023-02-10",NULL);
INSERT INTO clients VALUES (9,'Mercier', 'Henri', '0626272829', 'henri.mercier@mail.com', 'mdpmno', '5 rue de la Poste', '8901 2345 6789 0123', "2023-04-01",NULL);
INSERT INTO clients VALUES (10,'Dupuy', 'Isabelle', '0630313233', 'isabelle.dupuy@mail.com', 'mdppqr', '6 rue des Champs', '9012 3456 7890 1234', "2022-08-12",NULL);
INSERT INTO clients VALUES (11,'Berger', 'Jérôme', '0634353637', 'jerome.berger@mail.com', 'mdpstuv', '7 rue du Moulin', '0123 4567 8901 2345', "2023-01-05",NULL);
INSERT INTO clients VALUES (12,'Girard', 'Kevin', '0638394041', 'kevin.girard@mail.com', 'mdpwxy', '8 rue de la Forêt', '1234 5678 9012 3456', "2022-07-23",NULL);
INSERT INTO clients VALUES (13,'Lecomte', 'Laure', '0642434445', 'laure.lecomte@mail.com', 'mdp123', '9 rue de la Gare', '2345 6789 0123 4567', "2023-03-14",NULL);
INSERT INTO clients VALUES (14,'Roux', 'Matthieu', '0646474849', 'matthieu.roux@mail.com', 'mdp456', '10 rue des Roses', '3456 7890 1234 5678', "2022-12-02",NULL);
INSERT INTO clients VALUES (15,'Fontaine', 'Nathalie', '0650515253', 'nathalie.fontaine@mail.com', 'mdp789', '11 avenue des Acacias', '4567 8901 2345 6789', "2022-09-18",NULL);
INSERT INTO clients VALUES (16,'Leclerc', 'Olivier', '0654555657', 'olivier.leclerc@mail.com', 'mdpabc', '12 rue des Moulins', '5678 9012 3456 7890', "2023-05-02",NULL);

INSERT INTO clients VALUES (17,'Roy', 'Pauline', '0658596061', 'pauline.roy@mail.com', 'mdpdef', '13 rue de la Fontaine', '6789 0123 4567 8901', '2022-03-15',NULL);
INSERT INTO clients VALUES (18,'Gauthier', 'Quentin', '0662636465', 'quentin.gauthier@mail.com', 'mdpghi', '14 rue des Vignes', '7890 1234 5678 9012', '2022-04-23',NULL);
INSERT INTO clients VALUES (19,'Perrin', 'Romain', '0666676869', 'romain.perrin@mail.com', 'mdpjkl', '15 rue de la Plage', '8901 2345 6789 0123', '2022-06-01',NULL);
INSERT INTO clients VALUES (20,'Robin', 'Sophie', '0670717273', 'sophie.robin@mail.com', 'mdpmno', '16 avenue du Soleil', '9012 3456 7890 1234', '2022-05-10',NULL);
INSERT INTO clients VALUES (21,'Roger', 'Thomas', '0674757677', 'thomas.roger@mail.com', 'mdppqr', '17 rue du Lac', '0123 4567 8901 2345', '2023-01-20',NULL);
INSERT INTO clients VALUES
(22,'Dupont', 'Sophie', '0687654321', 'sophie.dupont@mail.com', 'mdpabc', '5 rue des Lilas', '3456 7890 1234 5678', '2022-06-15', NULL),
(23,'Martin', 'Alexandre', '0678654321', 'alex.martin@mail.com', 'mdplmn', '12 rue du Parc', '4567 8901 2345 6789', '2022-09-03', NULL),
(24,'Lefebvre', 'Jeanne', '0678456723', 'jeanne.lefebvre@mail.com', 'mdpdef', '23 rue du Temple', '7890 1234 5678 9012', '2022-12-07',NULL),
(25,'Moreau', 'Luc', '0687345698', 'luc.moreau@mail.com', 'mdpghe', '8 avenue des Roses', '9012 3456 7890 1234', '2023-04-02', NULL),
(26,'Durand', 'Caroline', '0678456723', 'caroline.durand@mail.com', 'mdpijk', '19 rue de la Paix', '2345 6789 0123 4567', '2022-11-11', NULL),
(27,'Simon', 'Marie', '0687678989', 'marie.simon@mail.com', 'mdpabc', '6 avenue des Champs', '5678 9012 3456 7890', '2022-03-25',NULL),
(28,'Garcia', 'Pierre', '0676453789', 'pierre.garcia@mail.com', 'mdplmn', '14 rue des Coquelicots', '7890 1234 5678 9012', '2023-02-14',NULL),
(29,'Fournier', 'Céline', '0687678989', 'celine.fournier@mail.com', 'mdpnmo', '10 avenue du Lac', '9012 3456 7890 1234', '2022-08-19', NULL),
(30,'Girard', 'Antoine', '0678989076', 'antoine.girard@mail.com', 'mdpabc', '11 rue de la Liberté', '2345 6789 0123 4567', '2023-05-01',NULL),
(31,'Mercier', 'Marie', '0687345698', 'marie.mercier@mail.com', 'mdpklm', '7 rue des Violettes', '4567 8901 2345 6789', '2023-03-10',NULL),
(32,'Roux', 'Paul', '0678456723', 'paul.roux@mail.com', 'mdpqwe', '15 avenue des Cerisiers', '7890 1234 5678 9012', '2022-10-02',NULL),
(33,'Giraud', 'Lucie', '0687678989', 'lucie.giraud@mail.com', 'mdpzxc', '2 rue des Peupliers', '9012 3456 7890 1234', '2023-04-23',NULL),
(34,'Bernard', 'Franck', '0676453789', 'franck.bernard@mail.com', 'mdpasd', '18 rue des Marronniers', '2345 6789 0123 4567', '2022-07-12',NULL),
(35,'Petit', 'Julie', '0688989898', 'julie.petit@mail.com', 'mdpjkl', '9 avenue des Lilas', '5678 9012 3456 7890', '2022-04-29',NULL),
(36,'Benoit', 'Nicolas', '0678989076', 'nicolas.benoit@mail.com', 'mdpzxc', '20 rue du Pont', '9012 3456 7890 1234', '2023-01-07',NULL),
(37,'Lemaire', 'Marie', '0687345698', 'marie.lemaire@mail.com', 'mdpklm', '22 avenue des Champs', '2345 6789 0123 4567', '2022-11-28',NULL),
(38,'Guillaume', 'Sophie', '0678456723', 'sophie.guillaume@mail.com', 'mdpqwe', '16 rue du Moulin', '7890 1234 5678 9012', '2023-03-01',NULL),
(39,'Fontaine', 'Jean', '0687678989', 'jean.fontaine@mail.com', 'mdpzxc', '3 rue des Acacias', '9012 3456 7890 1234', '2022-12-15',NULL),
(40,'Moreau', 'Alice', '0676453789', 'alice.moreau@mail.com', 'mdpasd', '12 avenue du Parc', '2345 6789 0123 4567', '2023-02-05',NULL);

#-----Commande-----
#Commande livrée. La commande a été livrée à l’adresse indiquée par le client. (CL)
INSERT INTO commande VALUES ("COM01", "2023-04-01", "2023-03-05", "2 Rue Imaginaire, Paris", "Joyeux anniversaire Romain!", "CL",50, 1, 2);
-- Commande standard pour laquelle un employé doit vérifier l’inventaire (VINV)
INSERT INTO commande VALUES ("COM02", "2023-04-15", "2023-04-17", "2 Rue Imaginaire, Paris", "Merci pour les chocolats !", "VINV",40, 1, 3);
-- Commande complète. Tous les items de la commande ont été indiqués (pour les commandes personnalisées) et tous ces items se trouvent en stock (CC)
INSERT INTO commande VALUES ("COM03", "2023-05-02", "2023-05-20", "3 Rue des Fleurs, Paris", "Joyeux anniversaire Alice !", "CC",80, 2, 2);
-- Commande personnalisée à vérifier. Lorsqu’un client passe une commande personnalisée, son état est « CPAV ». 
#Un employé vérifie la commande et rajoute des items si nécessaire. Ensuite, il change l’état de la commande à « CC » (CPAV)
INSERT INTO commande VALUES ("COM04", "2023-06-01", "2023-06-04", "5 Rue des Champs, Paris", "Félicitations pour votre mariage !", "CPAV",100, 3, 1);
-- Commande à livrer. La commande est prête ! (CAL)
INSERT INTO commande VALUES ("COM05", "2023-06-10", "2023-06-12", "7 Rue des Arbres, Paris", "Bonne fête des mères !", "CAL", 65, 4, 5);
-- Commande livrée. La commande a été livrée à l’adresse indiquée par le client. (CL)
INSERT INTO commande VALUES ("COM06", "2023-06-15", "2023-06-17", "1 rue de la Paix", "Bonne Saint-Valentin !", "CL",65, 5, 2);
-- Commande pour un client 
INSERT INTO commande VALUES ("COM07", "2023-07-01", "2023-07-04", "2 rue des Lilas", "Bonne fête !", "VINV",80, 5, 3);
-- Commande pour un client OR (achète plus de 5 bouquets par mois)
INSERT INTO commande VALUES ("COM08", "2023-07-10", "2023-07-12", "3 avenue de la République", "Bonne rentrée !", "CC", 43,2, 5);
-- Commande pour laquelle un employé doit vérifier l’inventaire (VINV)
INSERT INTO commande VALUES ("COM09", "2023-07-15", "2023-07-18", "4 rue de la Liberté", "Joyeux anniversaire Quentin !", "VINV",45, 3, 3);
INSERT INTO commande VALUES ("COM10", "2022-10-12", "2022-10-14", "14 rue des Fleurs", "Bonne saint-valentin Emma, j'espère que ces fleurs illumineront ta journée !", "CPAV",1000, 22, 2);
INSERT INTO commande VALUES ("COM11", "2022-11-05", "2022-11-07", "9 avenue des Champs-Élysées", "Tu es la meilleure !", "CC",1000, 23, 1);
INSERT INTO commande VALUES ("COM12", "2022-12-18", "2022-12-20", "12 rue de la Paix", "Joyeux fête Maman, ces fleurs sont pour toi !", "CAL",1000, 24, 4);
INSERT INTO commande VALUES ("COM13", "2023-01-03", "2023-01-06", "7 place du Marché", "Bonne année Pauline, que cette année t'apporte beaucoup de bonheur !", "CL",1000, 25, 5);
INSERT INTO commande VALUES ("COM14", "2023-02-14", "2023-02-16", "5 avenue de l'Opéra", "C'est pour toi !", "VINV",1000, 26, 3);
INSERT INTO commande VALUES ("COM15", "2023-03-21", "2023-03-23", "25 rue de Rivoli", "Joyeuse Saint-Valentin mon amour, ces fleurs sont pour toi !", "CPAV",1000, 27, 2);
INSERT INTO commande VALUES ("COM16", "2023-04-09", "2023-04-11", "2 rue de la République", "Bonne fête Grand-mère, ces fleurs sont pour toi !", "CAL",1000, 28, 1);
INSERT INTO commande VALUES ("COM17", "2023-05-01", "2023-05-03", "15 avenue des Ternes", "Joyeux 1er mai Caroline, ces fleurs sont pour toi !", "CL",1000, 29, 4);
INSERT INTO commande VALUES ("COM18", "2023-05-08", "2023-05-10", "8 rue de la Roquette", "Ces fleurs sont pour toi, pour te dire combien je t'aime !", "VINV",1000, 30, 5);
INSERT INTO commande VALUES ("COM19", "2023-05-15", "2023-05-17", "3 rue de la Paix", "Joyeux anniversaire Marc, j'espère que ces fleurs te feront plaisir !", "CC",1000, 31, 3);
INSERT INTO commande VALUES ("COM20", "2022-03-18", "2022-03-23", "12 rue des Lilas", "Merci pour tout !", "CPAV", 1000, 22, 1);
INSERT INTO commande VALUES ("COM21", "2022-04-02", "2022-04-05", "8 rue du Moulin", "Bonne fête grand-maman !", "VINV", 1000, 25, 2);
INSERT INTO commande VALUES ("COM22", "2022-04-14", "2022-04-17", "15 rue de la Paix", "Bonne fête papa !", "CAL", 65, 22, 4);
INSERT INTO commande VALUES ("COM23", "2022-05-01", "2022-05-05", "3 avenue des Roses", "Joyeuse fête du travail !", "CPAV", 1000, 26, 3);
INSERT INTO commande VALUES ("COM24", "2022-05-10", "2022-05-13", "7 rue de la Fontaine", "Bon rétablissement !", "CL", 1000, 23, 1);
INSERT INTO commande VALUES ("COM25", "2022-05-18", "2022-05-22", "9 avenue du Soleil", "Joyeux anniversaire de mariage !", "CC", 120, 24, 5);
INSERT INTO commande VALUES ("COM26", "2022-06-02", "2022-06-05", "2 rue des Cerisiers", "Merci pour tout !", "CAL", 1000, 28, 1);
INSERT INTO commande VALUES ("COM27", "2022-06-10", "2022-06-15", "16 avenue des Champs", "Bonne fête maman !", "CPAV", 1000, 29, 4);
INSERT INTO commande VALUES ("COM28", "2022-06-22", "2022-06-25", "6 rue du Lac", "Joyeux anniversaire de naissance !", "VINV", 80, 24, 3);
INSERT INTO commande VALUES ("COM29", "2022-07-05", "2022-07-08", "10 avenue des Tulipes", "Félicitations pour ton nouveau travail !", "CAL", 1000, 31, 2);

#-----Commande Standard-----
INSERT INTO commande_standard VALUES(1, "COM02", 3, 'Fabien');
INSERT INTO commande_standard VALUES(2, "COM09", 1, 'Yves');
INSERT INTO commande_standard VALUES(3, "COM07", 4, 'Jean');
INSERT INTO commande_standard VALUES(4, "COM05", 2, 'Laura');
INSERT INTO commande_standard VALUES(5, "COM06", 2, 'Yves');

INSERT INTO commande_standard VALUES (6,'COM10', 2, 'Laura');
INSERT INTO commande_standard VALUES (7,'COM11', 1, 'Julie');
INSERT INTO commande_standard VALUES (8,'COM12', 4, 'Sophie');
INSERT INTO commande_standard VALUES (9,'COM13', 5, 'Marie');
INSERT INTO commande_standard VALUES (10,'COM14', 3, 'Lucie');
INSERT INTO commande_standard VALUES (11,'COM15', 2, 'Laura');
INSERT INTO commande_standard VALUES (12,'COM16', 4, 'Sophie');
INSERT INTO commande_standard VALUES (13,'COM17', 1, 'Julie');
INSERT INTO commande_standard VALUES (14,'COM18', 5, 'Marie');
INSERT INTO commande_standard VALUES (15,'COM19', 3, 'Lucie');
INSERT INTO commande_standard VALUES (16,'COM20', 1, 'Julie');

#-----Commande Personnalisée-----
INSERT INTO commande_personnalisee VALUES (1, "COM01", "Un bouquet d'anniversaire avec des marguerites, des roses blanches, une vase et un ruban", 50);
INSERT INTO commande_personnalisee VALUES (2, "COM03", "Un bouquet d'anniversaire avec des gerberas, des roses roses et une touche de verdure", 80);
INSERT INTO commande_personnalisee VALUES (3, "COM04", "Un bouquet de mariage avec roses rouges, des lys, des oiseaux du paradis, une vases", 100);
INSERT INTO commande_personnalisee VALUES (4, "COM08","Un bouquet de rentrée avec des gerberas, des alstroméries, un panier et une carte de vœux",43);

#-----Confection-----
#Commande personnalisée n°1 avec un prix maximum de 50£
INSERT INTO confection VALUES (1, 12, 1);
INSERT INTO confection VALUES(1, 14, 1);
INSERT INTO confection VALUES(1, 4, 4);
INSERT INTO confection VALUES(1, 6, 3);

#Commande personnalisée n°2 avec un prix maximum de 80£
INSERT INTO confection VALUES (2, 1, 10);
INSERT INTO confection VALUES (2, 5, 12);

#Commande personnalisée n°3 avec un prix maximum de 100£
INSERT INTO confection VALUES (3, 12, 1); 
INSERT INTO confection VALUES (3, 5, 10); 
INSERT INTO confection VALUES (3, 9, 3); 
INSERT INTO confection VALUES (3, 7, 3);

#Commande personnalisée n°4 avec un prix maximum de 30£
INSERT INTO confection VALUES (4,1,2); 
INSERT INTO confection VALUES (4,10,5);
INSERT INTO confection VALUES (4,13,1);
INSERT INTO confection VALUES (4,16,1);

SELECT 'standard' AS type_commande, id_standard, num_commande, num_bouquet
FROM commande_standard 
UNION
SELECT 'personnalisée' AS type_commande, id_personnalisee, num_commande, NULL AS num_bouquet
FROM commande_personnalisee;

#Statistiques
#Calcul du prix moyen du bouquet acheté :
SELECT AVG(prix) FROM bouquet_standard 
INNER JOIN commande_standard 
ON bouquet_standard.num_bouquet = commande_standard.num_bouquet;

#Quel est le meilleur client du mois, de l’année :
#Pour le mois :
SELECT t1.id_client, (t1.total_achat + t2.total_achat) AS total_achat
FROM (SELECT id_client, SUM(bouquet_standard.prix) AS total_achat
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet
WHERE MONTH(date_commande) = MONTH('2023-04-01') AND YEAR(date_commande) = YEAR(CURRENT_DATE())
GROUP BY id_client) AS t1
JOIN (SELECT id_client, SUM(prix_max) AS total_achat
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
WHERE MONTH(date_commande) = MONTH('2023-04-01') AND YEAR(date_commande) = YEAR(CURRENT_DATE())
GROUP BY id_client) AS t2
ON t1.id_client = t2.id_client
ORDER BY total_achat DESC LIMIT 1;


#Pour l'année :
SELECT t1.id_client, (t1.total_achat + t2.total_achat) AS total_achat
FROM (SELECT id_client, SUM(bouquet_standard.prix) AS total_achat
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet
WHERE YEAR(date_commande) = YEAR(CURRENT_DATE())
GROUP BY id_client) AS t1
JOIN (SELECT id_client, SUM(prix_max) AS total_achat
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
WHERE YEAR(date_commande) = YEAR(CURRENT_DATE())
GROUP BY id_client) AS t2
ON t1.id_client = t2.id_client
ORDER BY total_achat DESC LIMIT 1;

#Quel est le bouquet standard qui a eu le plus de succès ?
SELECT num_bouquet, COUNT(*) AS nombre_de_commandes FROM commande_standard 
WHERE num_bouquet IN (SELECT num_bouquet FROM bouquet_standard) 
GROUP BY num_bouquet 
ORDER BY nombre_de_commandes DESC LIMIT 1;

#Quel est le magasin qui a généré le plus de chiffre d’affaires ?
SELECT num_boutique, SUM(bouquet_standard.prix) AS chiffre_affaires FROM commande_standard 
INNER JOIN commande 
ON commande_standard.num_commande = commande.num_commande 
INNER JOIN bouquet_standard
ON commande_standard.num_bouquet=bouquet_standard.num_bouquet
WHERE YEAR(date_commande) = YEAR(CURRENT_DATE()) 
GROUP BY num_boutique 
ORDER BY chiffre_affaires DESC LIMIT 1;

#Quelle est la fleur exotique la moins vendue ?
SELECT nom_item, SUM(quantite) AS quantite_vendue FROM items 
INNER JOIN composition 
ON items.id_items = composition.id_items 
WHERE nom_item IN ('Ginger','Gerbera')
GROUP BY nom_item 
ORDER BY quantite_vendue ASC LIMIT 1;


#Clients ayant commandé plus d'une fois dans le dernier mois
SELECT id_client, COUNT(*) AS nombre_commandes
FROM commande
WHERE date_commande >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH)
GROUP BY id_client
HAVING COUNT(*) > 1;

#Module statistiques

#Par mois :
#Nombre de commandes passées :
SELECT COUNT(*) AS nombre_commandes, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
GROUP BY mois, annee;

#Chiffre d'affaires total :
SELECT (t1.chiffre_affaires + t2.chiffre_affaires) AS chiffre_affaires, t1.mois, t1.annee
FROM (SELECT SUM(bouquet_standard.prix) AS chiffre_affaires, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet
GROUP BY mois, annee) AS t1
JOIN (SELECT SUM(prix_max) AS chiffre_affaires, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
GROUP BY mois, annee) AS t2
ON t1.mois = t2.mois;


#Nombre de bouquets vendus :
SELECT (t1.nombre_bouquets + t2.nombre_bouquets) AS nombre_bouquets, t1.mois, t1.annee
FROM (SELECT COUNT(*) AS nombre_bouquets, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet
GROUP BY mois, annee) AS t1
JOIN (SELECT COUNT(*) AS nombre_bouquets, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
GROUP BY mois, annee) AS t2
ON t1.mois = t2.mois AND t1.annee = t2.annee;

#Nombre de confections vendus :
SELECT (t1.nombre_bouquets + t2.nombre_confections) AS nombre_confections
FROM (SELECT COUNT(*) AS nombre_bouquets, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet) AS t1
JOIN (SELECT SUM(quantite) AS nombre_confections, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
INNER JOIN confection ON commande_personnalisee.id_personnalisee=confection.id_personnalisee) AS t2
ON t1.mois = t2.mois AND t1.annee = t2.annee;

#Nombre de clients nouveaux :
SELECT id_client
FROM clients
WHERE MONTH(date_inscription)=MONTH(CURRENT_DATE()) AND  YEAR(date_inscription)=YEAR(CURRENT_DATE());

#Par an :
#Nombre de commandes passées :
SELECT COUNT(*) AS nombre_commandes, YEAR(date_commande) AS annee
FROM commande
GROUP BY annee;

#Chiffre d'affaires total :

#Nombre de bouquets vendus :
SELECT COUNT(*) AS nombre_bouquets, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande
GROUP BY annee;

#Nombre de confections vendus :
SELECT SUM(quantite) AS nombre_confections, YEAR(date_commande) AS annee
FROM commande
INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande
INNER JOIN confection ON commande_personnalisee.id_personnalisee=confection.id_personnalisee
GROUP BY annee;

#Nombre de clients nouveaux :
#A compléter


#Sur les bouquets :
#Nombre de bouquets vendus par catégorie :
SELECT COUNT(*) AS nombre_bouquets, categorie
FROM bouquet_standard
INNER JOIN commande_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet
GROUP BY categorie;

#Prix moyen des bouquets vendus :
SELECT AVG(prix) AS prix_moyen
FROM bouquet_standard
INNER JOIN commande_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet;

#Bouquet le plus cher :
SELECT nom_bouquet, prix
FROM bouquet_standard
WHERE prix = (SELECT MAX(prix) FROM bouquet_standard);

#Bouquet le moins cher :
SELECT nom_bouquet, prix
FROM bouquet_standard
WHERE prix = (SELECT MIN(prix) FROM bouquet_standard);


#Sur les clients :
#Nombre de commandes passées par client :
SELECT id_client, COUNT(DISTINCT num_commande) AS nombre_commandes 
FROM commande 
GROUP BY id_client;

#Nombre de clients fidèles (ayant passé plus de 3 commandes) :
SELECT id_client 
FROM commande 
GROUP BY id_client 
HAVING COUNT(DISTINCT num_commande) > 3;

#Clients ayant effectué le plus d'achats :
SELECT id_client, COUNT(DISTINCT num_commande) AS nombre_commandes 
FROM commande 
GROUP BY id_client 
ORDER BY nombre_commandes DESC 
LIMIT 1;

#Clients ayant dépensé le plus :

#Sur les confections
#Nombre de confections vendus par catégorie :

#Prix moyen des confections vendus :
SELECT AVG(prix_max) AS prix_moyen 
FROM commande_personnalisee;

#confection le plus cher :
SELECT MAX(prix_max) AS prix_max 
FROM commande_personnalisee;

#confection le moins cher :
SELECT MIN(prix_max) AS prix_min
FROM commande_personnalisee;

#confection le plus vendu :
SELECT id_items, COUNT(*) AS nombre_ventes 
FROM commande_personnalisee 
INNER JOIN confection 
ON commande_personnalisee.id_personnalisee = confection.id_personnalisee 
GROUP BY id_items
ORDER BY nombre_ventes DESC 
LIMIT 1;

#confection le moins vendu :
SELECT id_items, COUNT(*) AS nombre_ventes 
FROM commande_personnalisee 
INNER JOIN confection 
ON commande_personnalisee.id_personnalisee = confection.id_personnalisee 
GROUP BY id_items 
ORDER BY nombre_ventes 
LIMIT 1;

#Sur les commandes :
#Nombre de commandes personnalisées :
SELECT COUNT(*) AS nombre_commandes 
FROM commande_personnalisee;

#Nombre de commandes standard :
SELECT COUNT(*) AS nombre_commandes
FROM commande_standard;

#Fidélite OR 
SELECT id_client, COUNT(*) AS nombre_commandes, MONTH(date_commande) as mois
FROM commande
GROUP BY id_client, mois
HAVING COUNT(*)>5;


#Fidélité Bronze
SELECT clients.id_client, COUNT(DISTINCT num_commande)/(TIMESTAMPDIFF(MONTH,date_inscription, date_commande)+1) AS nombre_commandes_par_mois
FROM clients NATURAL JOIN commande
GROUP BY clients.id_client
HAVING nombre_commandes_par_mois >=1;

SELECT id_client, COUNT(*) AS nb_bouquets_achetes
FROM commande
WHERE  MONTH(date_commande) = MONTH(CURDATE())
GROUP BY id_client
HAVING nb_bouquets_achetes >= 5;

UPDATE clients SET type_fidelite = 'OR';

SELECT id_client, COUNT(*) AS nb_bouquets_achetes, DATEDIFF(MAX(date_commande), MIN(date_commande))/30 AS nb_mois
FROM commande
GROUP BY id_client
HAVING (nb_bouquets_achetes / nb_mois) >= 1;

UPDATE clients SET type_fidelite = 'Bronze';

#Commande passée 3 jours avant date de livraison
SELECT num_commande
from commande
WHERE TIMESTAMPDIFF(DAY,date_commande,date_livraison)>3;

#Creation utilisateur bozo
DROP USER IF EXISTS 'bozo'@'localhost';
CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'bozo';
GRANT SELECT ON fleurs.* TO 'bozo'@'localhost';
#\connect bozo@localhost
#bozo
SHOW GRANTS FOR 'bozo'@'localhost';

#Requête synchronisée :
#On ajoute simplement une subquery pour trouver tous les clients ayant passé commande
SELECT * FROM clients WHERE EXISTS (SELECT * FROM commande WHERE clients.id_client = commande.id_client);


#Requête avec auto-jointure :
#On join clients à elle-même afin de trouver tous les clients ayant le même prénom, mais pas le même id_client
SELECT c1.id_client, c2.id_client
FROM clients c1
INNER JOIN clients c2 ON c1.id_client != c2.id_client AND c1.prenom_client = c2.prenom_client;

#Requête avec une UNION :
#On fait un répertoire de toutes les adresses, c'est-à-dire les adresses des clients et des boutiques
SELECT nom_client, adresse_facturation FROM clients UNION SELECT ville_boutique, adresse_boutique FROM boutique;

SELECT 'standard' AS type_commande, id_standard, commande.num_commande, commande.code_commande
FROM commande_standard INNER JOIN commande ON commande_standard.num_commande = commande.num_commande
WHERE commande.code_commande = "CAL"
UNION 
SELECT 'personnalisée' AS type_commande, commande_personnalisee.id_personnalisee, commande.num_commande, commande.code_commande 
FROM commande_personnalisee INNER JOIN commande ON commande_personnalisee.num_commande = commande.num_commande
WHERE commande.num_commande IN (
  SELECT num_commande FROM commande WHERE code_commande = "CAL"
);
select * from commande;

UPDATE commande SET code_commande = 'CL' WHERE date_livraison < CURDATE() AND code_commande = 'CAL';