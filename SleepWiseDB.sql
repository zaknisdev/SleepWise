CREATE DATABASE  SleepWiseDB;
USE SleepWiseDB;

CREATE TABLE ms_user (
    id_user INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL,
    nama_lengkap VARCHAR(100),
    role VARCHAR(10) NOT NULL, 
    target_tidur_jam INT DEFAULT 8
);

CREATE TABLE ms_kategori_tidur (
    id_kategori INT AUTO_INCREMENT PRIMARY KEY,
    nama_kategori VARCHAR(50),
    min_menit INT,
    max_menit INT,
    saran_harian TEXT
);

CREATE TABLE tr_log_tidur (
    id_log INT AUTO_INCREMENT PRIMARY KEY,
    id_user INT,
    id_kategori INT,
    tanggal DATE NOT NULL,
    jam_tidur DATETIME NOT NULL,
    jam_bangun DATETIME NOT NULL,
    durasi_menit INT,
    FOREIGN KEY (id_user) REFERENCES ms_user(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_kategori) REFERENCES ms_kategori_tidur(id_kategori) ON DELETE SET NULL
);


CREATE TABLE tr_evaluasi_mingguan (
    id_evaluasi INT AUTO_INCREMENT PRIMARY KEY,
    id_user INT,
    tanggal_rekap DATE NOT NULL,
    rata_durasi_menit INT,
    saran_jadwal_minggu_depan TEXT,
    FOREIGN KEY (id_user) REFERENCES ms_user(id_user) ON DELETE CASCADE
);


INSERT INTO ms_user (username, password, nama_lengkap, role, target_tidur_jam) VALUES 
('admin', 'admin123', 'Super Admin', 'Admin', 8);


INSERT INTO ms_kategori_tidur (nama_kategori, min_menit, max_menit, saran_harian) VALUES 
('Kurang', 0, 360, 'kurang lama tidurnya. Nanti kamu sakit sayang!!.'),
('Cukup', 361, 540, 'Tidurmu cukup. Tubuhmu akan terasa segar bugar beb!'),
('Berlebih', 541, 1440, 'Kelamaan lo tidurnya, blemes ntar.');

ALTER TABLE tr_log_tidur 
MODIFY COLUMN jam_tidur TIME NOT NULL,
MODIFY COLUMN jam_bangun TIME NOT NULL;
